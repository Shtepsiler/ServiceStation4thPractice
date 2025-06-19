import asyncio
import json
import time
from datetime import datetime, timedelta
from typing import Dict, List, Optional, Any, Tuple
import numpy as np
import structlog
from CONFIG import Config
from PydanticModels import PredictionRequest, FeedbackRequest


class ActiveLearningService:
    """Enhanced active learning service for automotive parts classification"""

    def __init__(self, redis_client, logger, retrain_service, config: Config):
        self.redis = redis_client
        self.logger = logger
        self.retrain_service = retrain_service
        self.config = config

        # Redis keys
        self.PREDICTIONS_KEY = "al:predictions"
        self.FEEDBACK_KEY = "al:feedback"
        self.UNCERTAINTY_QUEUE_KEY = "al:uncertainty_queue"
        self.STATS_KEY = "al:statistics"
        self.LEARNING_HISTORY_KEY = "al:learning_history"

        # Internal state
        self.uncertainty_samples = []
        self.feedback_buffer = []
        self.retrain_in_progress = False

        # Statistics tracking
        self.stats = {
            'total_predictions_stored': 0,
            'total_feedback_received': 0,
            'uncertainty_samples_collected': 0,
            'retrains_triggered': 0,
            'last_retrain_timestamp': None,
            'avg_confidence': 0.0,
            'feedback_accuracy': 0.0
        }

        self.logger.info("active_learning_service_initialized",
                         config=config.__dict__)

    def set_retrain_service(self, retrain_service):
        """Set retrain service reference"""
        self.retrain_service = retrain_service

    async def store_prediction(self, prediction_id: str, request: PredictionRequest,
                               predictions: List[Dict], confidence: float):
        """Store prediction for active learning analysis"""
        try:
            prediction_data = {
                'prediction_id': prediction_id,
                'problem_description': request.problem_description,
                'user_id': request.user_id,
                'predictions': predictions,
                'confidence': confidence,
                'timestamp': datetime.now().isoformat(),
                'language': request.language,
                'top_k': request.top_k,
                'threshold': request.confidence_threshold
            }

            # Store in Redis with expiry
            await self.redis.hset(
                self.PREDICTIONS_KEY,
                prediction_id,
                json.dumps(prediction_data)
            )
            await self.redis.expire(self.PREDICTIONS_KEY, 7 * 24 * 3600)  # 7 days

            # Check if prediction is uncertain
            if confidence < self.config.UNCERTAINTY_THRESHOLD:
                await self._add_to_uncertainty_queue(prediction_data)

            # Update statistics
            self.stats['total_predictions_stored'] += 1
            await self._update_statistics()

            self.logger.info("prediction_stored",
                             prediction_id=prediction_id,
                             confidence=confidence,
                             uncertain=confidence < self.config.UNCERTAINTY_THRESHOLD)

        except Exception as e:
            self.logger.error("store_prediction_failed",
                              prediction_id=prediction_id,
                              error=str(e))

    async def store_feedback(self, feedback: FeedbackRequest):
        """Store user feedback and trigger learning if needed"""
        try:
            feedback_data = {
                'prediction_id': feedback.prediction_id,
                'correct_parts': feedback.correct_parts,
                'user_rating': feedback.user_rating,
                'comments': feedback.comments,
                'user_id': feedback.user_id,
                'is_correct_prediction': feedback.is_correct_prediction,
                'time_to_feedback': feedback.time_to_feedback,
                'timestamp': datetime.now().isoformat()
            }

            # Store feedback
            await self.redis.hset(
                self.FEEDBACK_KEY,
                feedback.prediction_id,
                json.dumps(feedback_data)
            )
            await self.redis.expire(self.FEEDBACK_KEY, 30 * 24 * 3600)  # 30 days

            # Get original prediction
            prediction_data = await self._get_prediction_data(feedback.prediction_id)
            if prediction_data:
                # Create learning sample
                learning_sample = {
                    **prediction_data,
                    'feedback': feedback_data,
                    'learning_timestamp': datetime.now().isoformat()
                }

                # Store as learning data
                await self._store_learning_sample(learning_sample)

                # Check if retrain is needed
               # await self._check_retrain_trigger()

            self.stats['total_feedback_received'] += 1
            await self._update_statistics()

            self.logger.info("feedback_stored",
                             prediction_id=feedback.prediction_id,
                             rating=feedback.user_rating,
                             correct=feedback.is_correct_prediction)

        except Exception as e:
            self.logger.error("store_feedback_failed",
                              prediction_id=feedback.prediction_id,
                              error=str(e))

    async def get_uncertain_samples(self, limit: int = 10) -> List[Dict]:
        """Get uncertain predictions for human labeling"""
        try:
            # Get from uncertainty queue
            uncertain_keys = await self.redis.lrange(self.UNCERTAINTY_QUEUE_KEY, 0, limit - 1)

            samples = []
            for key in uncertain_keys:
                try:
                    sample_data = json.loads(key)
                    samples.append({
                        'prediction_id': sample_data['prediction_id'],
                        'problem_description': sample_data['problem_description'],
                        'predictions': sample_data['predictions'][:3],  # Top 3 only
                        'confidence': sample_data['confidence'],
                        'timestamp': sample_data['timestamp']
                    })
                except (json.JSONDecodeError, KeyError) as e:
                    self.logger.warning("uncertain_sample_parse_failed", error=str(e))
                    continue

            self.logger.info("uncertain_samples_retrieved", count=len(samples))
            return samples

        except Exception as e:
            self.logger.error("get_uncertain_samples_failed", error=str(e))
            return []

    async def get_learning_statistics(self) -> Dict[str, Any]:
        """Get comprehensive learning statistics"""
        try:
            # Get basic stats from Redis
            stats_data = await self.redis.get(self.STATS_KEY)
            if stats_data:
                stored_stats = json.loads(stats_data)
                self.stats.update(stored_stats)

            # Calculate additional metrics
            prediction_count = await self.redis.hlen(self.PREDICTIONS_KEY)
            feedback_count = await self.redis.hlen(self.FEEDBACK_KEY)
            uncertainty_queue_size = await self.redis.llen(self.UNCERTAINTY_QUEUE_KEY)

            # Calculate feedback accuracy
            feedback_accuracy = await self._calculate_feedback_accuracy()

            # Get retrain eligibility
            retrain_eligibility = {}
            if self.retrain_service:
                retrain_eligibility = await self.retrain_service.check_retrain_eligibility()

            comprehensive_stats = {
                'basic_stats': self.stats,
                'current_counts': {
                    'predictions_stored': prediction_count,
                    'feedback_received': feedback_count,
                    'uncertainty_queue_size': uncertainty_queue_size
                },
                'performance_metrics': {
                    'feedback_accuracy': feedback_accuracy,
                    'feedback_ratio': feedback_count / max(prediction_count, 1),
                    'uncertainty_ratio': uncertainty_queue_size / max(prediction_count, 1)
                },
                'retrain_info': retrain_eligibility,
                'timestamp': datetime.now().isoformat()
            }

            return comprehensive_stats

        except Exception as e:
            self.logger.error("get_learning_statistics_failed", error=str(e))
            return {'error': str(e)}

    async def get_health_status(self) -> Dict[str, Any]:
        """Get health status for monitoring"""
        try:
            redis_healthy = await self._check_redis_health()

            # Get queue sizes
            uncertainty_queue_size = await self.redis.llen(self.UNCERTAINTY_QUEUE_KEY)
            prediction_count = await self.redis.hlen(self.PREDICTIONS_KEY)
            feedback_count = await self.redis.hlen(self.FEEDBACK_KEY)

            status = {
                'service_healthy': redis_healthy and not self.retrain_in_progress,
                'redis_connected': redis_healthy,
                'retrain_in_progress': self.retrain_in_progress,
                'uncertainty_queue_size': uncertainty_queue_size,
                'prediction_count': prediction_count,
                'feedback_count': feedback_count,
                'last_retrain': self.stats.get('last_retrain_timestamp'),
                'uptime_stats': self.stats
            }

            return status

        except Exception as e:
            self.logger.error("health_status_failed", error=str(e))
            return {'service_healthy': False, 'error': str(e)}

    async def _add_to_uncertainty_queue(self, prediction_data: Dict):
        """Add uncertain prediction to queue for human review"""
        try:
            # Add to queue with size limit
            queue_size = await self.redis.llen(self.UNCERTAINTY_QUEUE_KEY)

            if queue_size >= 1000:  # Limit queue size
                await self.redis.rpop(self.UNCERTAINTY_QUEUE_KEY)  # Remove oldest

            await self.redis.lpush(
                self.UNCERTAINTY_QUEUE_KEY,
                json.dumps(prediction_data)
            )

            self.stats['uncertainty_samples_collected'] += 1

            self.logger.info("uncertainty_sample_queued",
                             prediction_id=prediction_data['prediction_id'],
                             confidence=prediction_data['confidence'])

        except Exception as e:
            self.logger.error("uncertainty_queue_failed", error=str(e))

    async def _get_prediction_data(self, prediction_id: str) -> Optional[Dict]:
        """Retrieve prediction data by ID"""
        try:
            prediction_raw = await self.redis.hget(self.PREDICTIONS_KEY, prediction_id)
            if prediction_raw:
                return json.loads(prediction_raw)
            return None
        except Exception as e:
            self.logger.warning("get_prediction_data_failed",
                                prediction_id=prediction_id,
                                error=str(e))
            return None

    async def _store_learning_sample(self, learning_sample: Dict):
        """Store combined prediction + feedback as learning data"""
        try:
            learning_key = f"learning:{learning_sample['prediction_id']}"
            await self.redis.set(
                learning_key,
                json.dumps(learning_sample),
                ex=90 * 24 * 3600  # 90 days retention
            )

            # Add to learning history for retrain triggering
            history_entry = {
                'prediction_id': learning_sample['prediction_id'],
                'timestamp': learning_sample['learning_timestamp'],
                'is_correct': learning_sample['feedback']['is_correct_prediction'],
                'confidence': learning_sample['confidence']
            }

            await self.redis.lpush(
                self.LEARNING_HISTORY_KEY,
                json.dumps(history_entry)
            )

            # Keep only recent history
            await self.redis.ltrim(self.LEARNING_HISTORY_KEY, 0, 999)

        except Exception as e:
            self.logger.error("store_learning_sample_failed", error=str(e))

    async def _check_retrain_trigger(self):
        """Check if conditions are met for retraining"""
        try:
            if self.retrain_in_progress or not self.retrain_service:
                return

            # Get recent feedback count
            recent_feedback = await self._get_recent_feedback_count()

            # Check trigger conditions
            should_retrain = (
                    recent_feedback >= self.config.FEEDBACK_THRESHOLD and
                    recent_feedback >= self.config.MIN_RETRAIN_SAMPLES
            )

            if should_retrain:
                # Check retrain eligibility with retrain service
                eligibility = await self.retrain_service.check_retrain_eligibility()

                if eligibility.get('eligible', False):
                    self.logger.info("retrain_trigger_conditions_met",
                                     recent_feedback=recent_feedback,
                                     threshold=self.config.FEEDBACK_THRESHOLD)

                    # Trigger retrain in background
                    asyncio.create_task(self._trigger_retrain())
                else:
                    self.logger.info("retrain_trigger_blocked",
                                     reason=eligibility.get('reason'))

        except Exception as e:
            self.logger.error("check_retrain_trigger_failed", error=str(e))

    async def _trigger_retrain(self):
        """Trigger retraining process"""
        try:
            if self.retrain_in_progress:
                self.logger.warning("retrain_already_in_progress")
                return

            self.retrain_in_progress = True
            self.logger.info("retrain_triggered")

            # Get learning data for retraining
            learning_data = await self._prepare_learning_data()

            if learning_data and len(learning_data) >= self.config.MIN_RETRAIN_SAMPLES:
                # Call retrain service
                retrain_result = await self.retrain_service.retrain_model(learning_data)

                if retrain_result.get('success', False):
                    self.stats['retrains_triggered'] += 1
                    self.stats['last_retrain_timestamp'] = datetime.now().isoformat()

                    # Clear processed learning samples
                    await self._cleanup_processed_samples(learning_data)

                    self.logger.info("retrain_completed_successfully",
                                     samples_used=len(learning_data),
                                     new_accuracy=retrain_result.get('accuracy'))
                else:
                    self.logger.error("retrain_failed",
                                      reason=retrain_result.get('error'))
            else:
                self.logger.warning("insufficient_learning_data",
                                    available=len(learning_data) if learning_data else 0,
                                    required=self.config.MIN_RETRAIN_SAMPLES)

        except Exception as e:
            self.logger.error("trigger_retrain_failed", error=str(e))
        finally:
            self.retrain_in_progress = False

    async def _prepare_learning_data(self) -> List[Dict]:
        """Prepare learning data from stored feedback"""
        try:
            learning_data = []

            # Get all learning samples
            learning_keys = await self.redis.keys("learning:*")

            for key in learning_keys:
                try:
                    sample_raw = await self.redis.get(key)
                    if sample_raw:
                        sample = json.loads(sample_raw)

                        # Format for training
                        formatted_sample = {
                            'problem_description': sample['problem_description'],
                            'correct_parts': sample['feedback']['correct_parts'],
                            'original_predictions': sample['predictions'],
                            'confidence': sample['confidence'],
                            'is_correct': sample['feedback']['is_correct_prediction'],
                            'user_rating': sample['feedback'].get('user_rating'),
                            'timestamp': sample['learning_timestamp']
                        }

                        learning_data.append(formatted_sample)

                except (json.JSONDecodeError, KeyError) as e:
                    self.logger.warning("learning_sample_parse_failed",
                                        key=key, error=str(e))
                    continue

            # Sort by timestamp (newest first) and limit
            learning_data.sort(key=lambda x: x['timestamp'], reverse=True)
            learning_data = learning_data[:500]  # Limit for memory

            self.logger.info("learning_data_prepared",
                             total_samples=len(learning_data))

            return learning_data

        except Exception as e:
            self.logger.error("prepare_learning_data_failed", error=str(e))
            return []

    async def _cleanup_processed_samples(self, processed_data: List[Dict]):
        """Clean up processed learning samples"""
        try:
            # Remove processed samples from learning store
            for sample in processed_data:
                if 'prediction_id' in sample:
                    learning_key = f"learning:{sample['prediction_id']}"
                    await self.redis.delete(learning_key)

            self.logger.info("processed_samples_cleaned", count=len(processed_data))

        except Exception as e:
            self.logger.error("cleanup_samples_failed", error=str(e))

    async def _get_recent_feedback_count(self, hours: int = 24) -> int:
        """Get count of recent feedback"""
        try:
            cutoff_time = datetime.now() - timedelta(hours=hours)
            recent_count = 0

            # Get learning history
            history_entries = await self.redis.lrange(self.LEARNING_HISTORY_KEY, 0, -1)

            for entry_raw in history_entries:
                try:
                    entry = json.loads(entry_raw)
                    entry_time = datetime.fromisoformat(entry['timestamp'])

                    if entry_time > cutoff_time:
                        recent_count += 1
                    else:
                        break  # History is sorted by time

                except (json.JSONDecodeError, ValueError):
                    continue

            return recent_count

        except Exception as e:
            self.logger.error("get_recent_feedback_count_failed", error=str(e))
            return 0

    async def _calculate_feedback_accuracy(self) -> float:
        """Calculate feedback accuracy from recent data"""
        try:
            total_feedback = 0
            correct_feedback = 0

            # Get recent learning history
            history_entries = await self.redis.lrange(self.LEARNING_HISTORY_KEY, 0, 99)

            for entry_raw in history_entries:
                try:
                    entry = json.loads(entry_raw)
                    total_feedback += 1
                    if entry.get('is_correct', False):
                        correct_feedback += 1
                except (json.JSONDecodeError, KeyError):
                    continue

            if total_feedback == 0:
                return 0.0

            accuracy = correct_feedback / total_feedback
            self.stats['feedback_accuracy'] = accuracy

            return accuracy

        except Exception as e:
            self.logger.error("calculate_feedback_accuracy_failed", error=str(e))
            return 0.0

    async def _update_statistics(self):
        """Update stored statistics"""
        try:
            await self.redis.set(
                self.STATS_KEY,
                json.dumps(self.stats),
                ex=24 * 3600  # 24 hours
            )
        except Exception as e:
            self.logger.warning("update_statistics_failed", error=str(e))

    async def _check_redis_health(self) -> bool:
        """Check Redis connectivity"""
        try:
            await self.redis.ping()
            return True
        except:
            return False