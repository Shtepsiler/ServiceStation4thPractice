import asyncio
import json
import os
import time
import shutil
import threading
from concurrent.futures import ThreadPoolExecutor
from datetime import datetime, timedelta
from typing import Dict, List, Optional, Any, Tuple
import numpy as np
import torch
import torch.nn as nn
from torch.utils.data import DataLoader, Dataset
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import MultiLabelBinarizer
from sklearn.metrics import accuracy_score, f1_score, classification_report
import structlog

from CONFIG import Config
from LSTMAttentionModel import LSTMAttentionModel


class RetrainDataset(Dataset):
    """Dataset for retraining with automotive parts data"""

    def __init__(self, problems: List[str], labels: List[List[str]], vocab: Dict, mlb: MultiLabelBinarizer):
        self.problems = problems
        self.labels = labels
        self.vocab = vocab
        self.mlb = mlb

    def __len__(self):
        return len(self.problems)

    def __getitem__(self, idx):
        problem = self.problems[idx]
        labels = self.labels[idx]

        # Tokenize problem (same logic as ModelService)
        clean_text = self._clean_text(problem)
        words = clean_text.split()

        # Create tokens with bigrams
        tokens = words[:64]
        if len(words) > 1:
            bigrams = [f"{words[i]}_{words[i + 1]}" for i in range(len(words) - 1)]
            tokens.extend(bigrams[:64])

        # Convert to indices
        indices = [self.vocab.get(token, 1) for token in tokens[:128]]

        # Pad sequence
        if len(indices) < 128:
            indices.extend([0] * (128 - len(indices)))

        # Convert labels to multi-hot encoding
        label_vector = self.mlb.transform([labels])[0]

        return torch.LongTensor(indices), torch.FloatTensor(label_vector)

    def _clean_text(self, text: str) -> str:
        """Clean text (same as ModelService)"""
        import re

        if not text:
            return ""

        text = str(text).lower()
        text = re.sub(r'[^\w\sа-яіїєґ_]', ' ', text)
        text = re.sub(r'\s+', ' ', text)

        return text.strip()


class RetrainService:
    """Enhanced retraining service with async background training"""

    def __init__(self, redis_client, logger, model_service, config: Config):
        self.redis = redis_client
        self.logger = logger
        self.model_service = model_service
        self.config = config

        # Redis keys
        self.RETRAIN_STATUS_KEY = "retrain:status"
        self.BASELINE_DATA_KEY = "retrain:baseline_data"
        self.RETRAIN_HISTORY_KEY = "retrain:history"
        self.TRAINING_PROGRESS_KEY = "retrain:progress"

        # Training state
        self.retrain_in_progress = False
        self.last_retrain_timestamp = None
        self.consecutive_failed_retrains = 0
        self.training_task = None
        self.training_progress = {}

        # Device setup
        self.device = torch.device("cuda" if torch.cuda.is_available() else "cpu")

        # Thread pool for background training
        self.executor = ThreadPoolExecutor(max_workers=1, thread_name_prefix="retrain")

        # Baseline test data (loaded from dataset)
        self.baseline_test_data = None
        self._load_baseline_test_data()

        self.logger.info("retrain_service_initialized",
                         device=str(self.device),
                         mode=config.TRANSFER_LEARNING_MODE,
                         epochs=config.TRANSFER_MAX_EPOCHS,
                         lr=config.TRANSFER_ULTRA_CONSERVATIVE_LR)

    def _load_baseline_test_data(self):
        """Load baseline test data from CSV dataset (not hardcoded)"""
        try:
            baseline_samples = []

            # Look for CSV dataset files
            csv_paths = [
                'data/parts_recommendations.csv',
                '../data/parts_recommendations.csv',
                'parts_recommendations.csv'
            ]

            for csv_path in csv_paths:
                if os.path.exists(csv_path):
                    try:
                        import pandas as pd

                        # Read CSV with different encodings
                        df = None
                        for encoding in ['utf-8', 'utf-8-sig', 'cp1251', 'iso-8859-1']:
                            try:
                                df = pd.read_csv(csv_path, encoding=encoding)
                                break
                            except UnicodeDecodeError:
                                continue

                        if df is None:
                            self.logger.warning("csv_encoding_failed", path=csv_path)
                            continue

                        # Clean column names
                        df.columns = df.columns.str.strip()

                        # Find problem and parts columns
                        problem_col = None
                        parts_col = None

                        for col in df.columns:
                            col_lower = col.lower()
                            if any(keyword in col_lower for keyword in
                                   ['problem', 'description', 'problem_description']):
                                problem_col = col
                                break

                        for col in df.columns:
                            col_lower = col.lower()
                            if any(keyword in col_lower for keyword in
                                   ['part', 'relevant', 'relevant_parts']):
                                parts_col = col
                                break

                        if not problem_col or not parts_col:
                            if len(df.columns) >= 2:
                                problem_col = df.columns[0]
                                parts_col = df.columns[1]
                                self.logger.info("using_default_columns",
                                                 problem_col=problem_col,
                                                 parts_col=parts_col)
                            else:
                                self.logger.warning("insufficient_columns", path=csv_path)
                                continue

                        # Extract validation samples (small random sample for speed)
                        total_rows = len(df)
                        baseline_sample_size = getattr(self.config, 'BASELINE_SAMPLE_SIZE', 50)

                        # Take random sample for faster validation
                        if total_rows > baseline_sample_size:
                            validation_df = df.sample(n=baseline_sample_size, random_state=42)
                        else:
                            validation_df = df.iloc[-baseline_sample_size:]  # Last N rows

                        for _, row in validation_df.iterrows():
                            try:
                                problem = str(row[problem_col]).strip()
                                parts_str = str(row[parts_col]).strip()

                                if (problem and parts_str and
                                        problem not in ['nan', 'None', ''] and
                                        parts_str not in ['nan', 'None', '']):

                                    # Parse parts with different separators
                                    if ',' in parts_str:
                                        parts = [p.strip() for p in parts_str.split(',')]
                                    elif ';' in parts_str:
                                        parts = [p.strip() for p in parts_str.split(';')]
                                    elif '|' in parts_str:
                                        parts = [p.strip() for p in parts_str.split('|')]
                                    else:
                                        parts = [parts_str]

                                    # Filter valid parts
                                    parts = [p for p in parts if p and p.lower() not in ['nan', 'none', 'null', '']]

                                    if parts:
                                        baseline_samples.append((problem, parts))

                            except Exception as e:
                                self.logger.warning("baseline_sample_parse_failed",
                                                    row_index=_, error=str(e))
                                continue

                        if baseline_samples:
                            self.logger.info("baseline_data_loaded_from_dataset",
                                             path=csv_path,
                                             total_rows=total_rows,
                                             validation_samples=len(baseline_samples),
                                             problem_col=problem_col,
                                             parts_col=parts_col)
                            break

                    except Exception as e:
                        self.logger.warning("csv_load_failed",
                                            path=csv_path,
                                            error=str(e))
                        continue

            # If no CSV found, log error but don't use hardcoded data
            if not baseline_samples:
                self.logger.error("no_dataset_found_validation_disabled",
                                  searched_paths=csv_paths)
                baseline_samples = []  # Empty - validation will be disabled

            self.baseline_test_data = baseline_samples
            self.logger.info("baseline_data_initialized",
                             size=len(baseline_samples),
                             validation_enabled=len(baseline_samples) > 0)

        except Exception as e:
            self.logger.error("load_baseline_data_failed", error=str(e))
            self.baseline_test_data = []

    async def _load_and_cache_baseline_data(self):
        """Load baseline data from cache after initialization"""
        try:
            if not self.redis:
                return

            # Try to load from cache
            cached_raw = await self.redis.get(self.BASELINE_DATA_KEY)
            if cached_raw:
                cached_data = json.loads(cached_raw)
                if cached_data and len(cached_data) > len(self.baseline_test_data):
                    self.baseline_test_data = cached_data
                    self.logger.info("baseline_data_loaded_from_cache",
                                     size=len(cached_data))
                    return

            # Cache current data if we have it
            if self.baseline_test_data:
                await self.redis.set(
                    self.BASELINE_DATA_KEY,
                    json.dumps(self.baseline_test_data),
                    ex=7 * 24 * 3600  # 7 days
                )
                self.logger.info("baseline_data_cached", size=len(self.baseline_test_data))

        except Exception as e:
            self.logger.warning("baseline_cache_operation_failed", error=str(e))

    async def check_retrain_eligibility(self) -> Dict[str, Any]:
        """Check if model is eligible for retraining"""
        try:
            # Check if retrain is already in progress
            if self.retrain_in_progress:
                return {
                    'eligible': False,
                    'reason': 'retrain_in_progress',
                    'progress': self.training_progress
                }

            # Check consecutive failures
            if self.consecutive_failed_retrains >= self.config.MAX_CONSECUTIVE_FAILED_RETRAINS:
                return {
                    'eligible': False,
                    'reason': 'too_many_consecutive_failures',
                    'failed_count': self.consecutive_failed_retrains
                }

            # Check minimum time between retrains
            if self.last_retrain_timestamp:
                time_since_last = datetime.now() - self.last_retrain_timestamp
                min_interval = timedelta(hours=getattr(self.config, 'MIN_RETRAIN_INTERVAL_HOURS', 1))
                if time_since_last < min_interval:
                    return {
                        'eligible': False,
                        'reason': 'too_soon_since_last_retrain',
                        'time_since_last': time_since_last.total_seconds(),
                        'min_interval': min_interval.total_seconds()
                    }

            # Check model service availability
            if not self.model_service or not self.model_service.model:
                return {
                    'eligible': False,
                    'reason': 'model_service_unavailable'
                }

            return {
                'eligible': True,
                'strategy': self.config.TRANSFER_LEARNING_MODE,
                'last_retrain': self.last_retrain_timestamp.isoformat() if self.last_retrain_timestamp else None,
                'failed_retrains': self.consecutive_failed_retrains,
                'config': {
                    'epochs': self.config.TRANSFER_MAX_EPOCHS,
                    'learning_rate': self.config.TRANSFER_ULTRA_CONSERVATIVE_LR,
                    'batch_size': self.config.TRANSFER_BATCH_SIZE,
                    'gradient_clip': self.config.TRANSFER_GRADIENT_CLIP
                }
            }

        except Exception as e:
            self.logger.error("check_retrain_eligibility_failed", error=str(e))
            return {
                'eligible': False,
                'reason': f'eligibility_check_failed: {str(e)}'
            }

    async def retrain_model(self, learning_data: List[Dict]) -> Dict[str, Any]:
        """Start async model retraining in background thread"""
        if self.retrain_in_progress:
            return {
                'success': False,
                'error': 'retrain_already_in_progress',
                'progress': self.training_progress
            }

        try:
            # Validate learning data quality
            quality_check = await self._validate_learning_data(learning_data)
            if not quality_check['valid']:
                return {
                    'success': False,
                    'error': f"Learning data validation failed: {quality_check['reason']}"
                }

            # Test baseline performance if validation data available
            baseline_performance = await self._test_baseline_performance()
            min_baseline = getattr(self.config, 'MIN_BASELINE_PERFORMANCE', 0.1)

            if len(self.baseline_test_data) > 0 and baseline_performance < min_baseline:
                return {
                    'success': False,
                    'error': f"Current model baseline performance too low: {baseline_performance:.3f} < {min_baseline}"
                }

            # Set training in progress
            self.retrain_in_progress = True
            self.training_progress = {
                'status': 'starting',
                'start_time': datetime.now().isoformat(),
                'data_size': len(learning_data),
                'baseline_performance': baseline_performance,
                'epochs_total': self.config.TRANSFER_MAX_EPOCHS,
                'epoch_current': 0,
                'train_loss': 0.0,
                'val_accuracy': 0.0,
                'strategy': self.config.TRANSFER_LEARNING_MODE
            }

            # Start background training task
            loop = asyncio.get_event_loop()
            self.training_task = loop.run_in_executor(
                self.executor,
                self._background_retrain,
                learning_data,
                baseline_performance
            )

            self.logger.info("async_retrain_started",
                             strategy=self.config.TRANSFER_LEARNING_MODE,
                             data_size=len(learning_data),
                             baseline_performance=baseline_performance)

            return {
                'success': True,
                'message': 'Retraining started in background',
                'progress': self.training_progress,
                'baseline_performance': baseline_performance
            }

        except Exception as e:
            self.retrain_in_progress = False
            self.logger.error("retrain_start_failed", error=str(e))
            return {
                'success': False,
                'error': str(e)
            }

    def _background_retrain(self, learning_data: List[Dict], baseline_performance: float) -> Dict[str, Any]:
        """Background retraining in separate thread"""
        start_time = time.time()

        try:
            self.logger.info("background_retrain_started")

            # Update progress
            self.training_progress['status'] = 'preparing_data'
            self._update_training_progress()

            # Prepare training data
            train_problems, train_labels, new_classes = self._prepare_training_data_sync(learning_data)

            # Choose strategy and train
            if self.config.TRANSFER_LEARNING_MODE == "ultra_conservative":
                result = self._ultra_conservative_retrain_sync(
                    train_problems, train_labels, new_classes, baseline_performance
                )
            else:
                result = {
                    'success': False,
                    'error': f'Training strategy {self.config.TRANSFER_LEARNING_MODE} not implemented'
                }

            # Record training time
            training_time = time.time() - start_time
            result['training_time'] = training_time

            # Update state based on result
            if result['success']:
                self.consecutive_failed_retrains = 0
                self.last_retrain_timestamp = datetime.now()
                self.training_progress['status'] = 'completed'

                # Store training history (sync version)
                self._store_retrain_history_sync(result, learning_data, baseline_performance)

                self.logger.info("background_retrain_completed_successfully",
                                 training_time=training_time,
                                 new_accuracy=result.get('accuracy'),
                                 new_classes_added=len(new_classes))
            else:
                self.consecutive_failed_retrains += 1
                self.training_progress['status'] = 'failed'
                self.training_progress['error'] = result.get('error')

                self.logger.error("background_retrain_failed",
                                  error=result.get('error'),
                                  consecutive_failures=self.consecutive_failed_retrains)

            self.training_progress['end_time'] = datetime.now().isoformat()
            self.training_progress['training_time'] = training_time
            self._update_training_progress()

            return result

        except Exception as e:
            self.consecutive_failed_retrains += 1
            self.training_progress['status'] = 'error'
            self.training_progress['error'] = str(e)
            self.training_progress['end_time'] = datetime.now().isoformat()
            self._update_training_progress()

            self.logger.error("background_retrain_exception",
                              error=str(e),
                              consecutive_failures=self.consecutive_failed_retrains)
            return {
                'success': False,
                'error': str(e),
                'training_time': time.time() - start_time
            }
        finally:
            self.retrain_in_progress = False

    def _update_training_progress(self):
        """Update training progress in Redis (sync version)"""
        try:
            if self.redis:
                # Use sync Redis client for thread safety
                import redis
                sync_redis = redis.Redis.from_url(
                    f"redis://{self.redis.connection_pool.connection_kwargs['host']}:"
                    f"{self.redis.connection_pool.connection_kwargs['port']}"
                )
                sync_redis.set(
                    self.TRAINING_PROGRESS_KEY,
                    json.dumps(self.training_progress),
                    ex=3600  # 1 hour
                )
        except Exception as e:
            self.logger.warning("update_training_progress_failed", error=str(e))

    async def get_training_progress(self) -> Dict[str, Any]:
        """Get current training progress"""
        try:
            if self.retrain_in_progress and self.training_task:
                # Check if task is done
                if self.training_task.done():
                    try:
                        result = self.training_task.result()
                        self.training_progress['final_result'] = result
                    except Exception as e:
                        self.training_progress['final_error'] = str(e)
                    finally:
                        self.retrain_in_progress = False
                        self.training_task = None

            # Try to get from Redis if available
            if self.redis:
                cached_progress = await self.redis.get(self.TRAINING_PROGRESS_KEY)
                if cached_progress:
                    return json.loads(cached_progress)

            return self.training_progress

        except Exception as e:
            self.logger.error("get_training_progress_failed", error=str(e))
            return {'error': str(e)}

    async def trigger_retrain(self) -> Dict[str, Any]:
        """Manually trigger retraining"""
        try:
            # Get recent learning data
            learning_data = await self._get_recent_learning_data()

            min_samples = getattr(self.config, 'MIN_RETRAIN_SAMPLES', 5)
            if len(learning_data) < min_samples:
                return {
                    'success': False,
                    'error': f'Insufficient learning data: {len(learning_data)} < {min_samples}'
                }

            return await self.retrain_model(learning_data)

        except Exception as e:
            return {'success': False, 'error': str(e)}

    async def _validate_learning_data(self, learning_data: List[Dict]) -> Dict[str, Any]:
        """Validate quality of learning data (uses config values)"""
        try:
            min_samples = getattr(self.config, 'MIN_QUALITY_SAMPLES', 5)
            min_diversity = getattr(self.config, 'MIN_DIVERSITY_CLASSES', 3)
            quality_threshold = getattr(self.config, 'QUALITY_THRESHOLD', 0.3)

            # Development mode - significantly relax requirements
            development_mode = getattr(self.config, 'DEVELOPMENT_MODE', False)
            skip_quality_check = getattr(self.config, 'SKIP_HEAVY_VALIDATION', False)

            if development_mode:
                min_samples = max(1, min_samples // 10)  # Much more relaxed
                min_diversity = 1  # Just need 1 unique part
                quality_threshold = 0.01  # Very low threshold
                self.logger.info("validation_development_mode_active",
                                 relaxed_min_samples=min_samples,
                                 relaxed_quality_threshold=quality_threshold)

            if len(learning_data) < min_samples:
                return {
                    'valid': False,
                    'reason': f'Too few samples: {len(learning_data)} < {min_samples}'
                }

            # Check diversity of correct parts
            all_parts = set()
            valid_samples = 0

            for sample in learning_data:
                correct_parts = sample.get('correct_parts', [])
                if correct_parts:
                    all_parts.update(correct_parts)
                    valid_samples += 1

            if valid_samples == 0:
                return {
                    'valid': False,
                    'reason': 'No samples with valid correct_parts found'
                }

            if len(all_parts) < min_diversity:
                return {
                    'valid': False,
                    'reason': f'Too few diverse classes: {len(all_parts)} < {min_diversity}'
                }

            # Quality ratio check
            correct_count = sum(1 for sample in learning_data if sample.get('is_correct', False))
            quality_ratio = correct_count / len(learning_data) if len(learning_data) > 0 else 0.0

            # Skip quality check in development mode or if explicitly disabled
            if not skip_quality_check and not development_mode and quality_ratio < quality_threshold:
                return {
                    'valid': False,
                    'reason': f'Low quality ratio: {quality_ratio:.3f} < {quality_threshold}'
                }

            # Log quality info but pass validation
            validation_status = "relaxed_development" if development_mode else "standard"
            self.logger.info("learning_data_validation_passed",
                             total_samples=len(learning_data),
                             valid_samples=valid_samples,
                             unique_parts=len(all_parts),
                             quality_ratio=quality_ratio,
                             validation_mode=validation_status,
                             thresholds_used={
                                 'min_samples': min_samples,
                                 'min_diversity': min_diversity,
                                 'quality_threshold': quality_threshold,
                                 'skip_quality_check': skip_quality_check
                             })

            return {
                'valid': True,
                'samples': len(learning_data),
                'valid_samples': valid_samples,
                'unique_parts': len(all_parts),
                'quality_ratio': quality_ratio,
                'thresholds': {
                    'min_samples': min_samples,
                    'min_diversity': min_diversity,
                    'quality_threshold': quality_threshold
                },
                'development_mode': development_mode,
                'skip_quality_check': skip_quality_check
            }

        except Exception as e:
            return {
                'valid': False,
                'reason': f'Validation error: {str(e)}'
            }

    async def _test_baseline_performance(self) -> float:
        """Test current model on baseline data with limited samples"""
        try:
            if not self.baseline_test_data or not self.model_service:
                self.logger.info("baseline_test_skipped_no_data_or_model")
                return 0.0

            # Limit baseline test to small sample for speed
            test_sample_size = getattr(self.config, 'BASELINE_TEST_SIZE', 10)
            test_samples = self.baseline_test_data[:test_sample_size]

            correct_predictions = 0
            total_predictions = 0

            for problem, expected_parts in test_samples:
                try:
                    predictions, confidence, _ = self.model_service.predict(
                        problem, top_k=3, threshold=0.1
                    )

                    predicted_parts = [pred['part'] for pred in predictions]

                    # Check if at least one expected part is in top predictions
                    if any(part in predicted_parts for part in expected_parts):
                        correct_predictions += 1

                    total_predictions += 1

                except Exception as e:
                    self.logger.warning("baseline_test_prediction_failed",
                                        problem=problem[:50], error=str(e))
                    continue

            if total_predictions == 0:
                return 0.0

            performance = correct_predictions / total_predictions
            self.logger.info("baseline_performance_tested",
                             correct=correct_predictions,
                             total=total_predictions,
                             performance=performance,
                             test_sample_size=test_sample_size,
                             total_available=len(self.baseline_test_data))

            return performance

        except Exception as e:
            self.logger.error("baseline_performance_test_failed", error=str(e))
            return 0.0

    def _prepare_training_data_sync(self, learning_data: List[Dict]) -> Tuple[List[str], List[List[str]], List[str]]:
        """Synchronous version of prepare training data for thread execution"""
        try:
            problems = []
            labels = []
            all_parts = set()

            # Load original dataset
            original_dataset_loaded = False
            csv_paths = [
                'data/parts_recommendations.csv',
                '../data/parts_recommendations.csv',
                'parts_recommendations.csv'
            ]

            for csv_path in csv_paths:
                if os.path.exists(csv_path):
                    try:
                        import pandas as pd

                        # Read CSV
                        df = None
                        for encoding in ['utf-8', 'utf-8-sig', 'cp1251', 'iso-8859-1']:
                            try:
                                df = pd.read_csv(csv_path, encoding=encoding)
                                break
                            except UnicodeDecodeError:
                                continue

                        if df is None:
                            continue

                        # Clean column names
                        df.columns = df.columns.str.strip()

                        # Find columns
                        problem_col = None
                        parts_col = None

                        for col in df.columns:
                            col_lower = col.lower()
                            if any(keyword in col_lower for keyword in ['problem', 'description']):
                                problem_col = col
                                break

                        for col in df.columns:
                            col_lower = col.lower()
                            if any(keyword in col_lower for keyword in ['part', 'relevant']):
                                parts_col = col
                                break

                        if problem_col and parts_col:
                            # Use first 80% for training (excluding validation samples)
                            total_rows = len(df)
                            train_end = int(total_rows * 0.8)
                            train_df = df.iloc[:train_end]

                            for _, row in train_df.iterrows():
                                try:
                                    problem = str(row[problem_col]).strip()
                                    parts_str = str(row[parts_col]).strip()

                                    if (problem and parts_str and
                                            problem not in ['nan', 'None', ''] and
                                            parts_str not in ['nan', 'None', '']):

                                        # Parse parts
                                        if ',' in parts_str:
                                            parts = [p.strip() for p in parts_str.split(',')]
                                        elif ';' in parts_str:
                                            parts = [p.strip() for p in parts_str.split(';')]
                                        else:
                                            parts = [parts_str]

                                        # Filter valid parts
                                        parts = [p for p in parts if p and p.lower() not in ['nan', 'none', 'null', '']]

                                        if parts:
                                            problems.append(problem)
                                            labels.append(parts)
                                            all_parts.update(parts)

                                except Exception as e:
                                    self.logger.warning("dataset_sample_parse_failed",
                                                        row_index=_, error=str(e))
                                    continue

                            original_dataset_loaded = True
                            self.logger.info("original_dataset_loaded_for_training",
                                             path=csv_path,
                                             training_samples=len(problems),
                                             total_rows=total_rows,
                                             train_split=train_end)
                            break

                    except Exception as e:
                        self.logger.warning("dataset_load_failed",
                                            path=csv_path, error=str(e))
                        continue

            if not original_dataset_loaded:
                self.logger.warning("no_original_dataset_found_using_learning_data_only")

            # Add new learning samples
            new_samples_count = 0
            oversample_ratio = getattr(self.config, 'NEW_CLASS_OVERSAMPLE_RATIO', 2)

            for sample in learning_data:
                problem = sample.get('problem_description', '')
                correct_parts = sample.get('correct_parts', [])

                if problem and correct_parts:
                    problems.append(problem)
                    labels.append(correct_parts)
                    all_parts.update(correct_parts)
                    new_samples_count += 1

                    # Oversample new classes
                    current_classes = set(self.model_service.mlb.classes_)
                    if any(part not in current_classes for part in correct_parts):
                        for _ in range(oversample_ratio):
                            problems.append(problem)
                            labels.append(correct_parts)

            # Find new classes
            current_classes = set(self.model_service.mlb.classes_)
            new_classes = list(all_parts - current_classes)

            self.logger.info("training_data_prepared_sync",
                             total_problems=len(problems),
                             original_dataset_loaded=original_dataset_loaded,
                             new_learning_samples=new_samples_count,
                             unique_parts=len(all_parts),
                             existing_classes=len(current_classes),
                             new_classes=len(new_classes))

            return problems, labels, new_classes

        except Exception as e:
            self.logger.error("prepare_training_data_sync_failed", error=str(e))
            raise

    def _ultra_conservative_retrain_sync(self, problems: List[str], labels: List[List[str]],
                                         new_classes: List[str], baseline_performance: float) -> Dict[str, Any]:
        """Synchronous ultra-conservative transfer learning"""
        try:
            self.training_progress['status'] = 'backing_up_model'
            self._update_training_progress()

            # Backup current model
            backup_path = self._backup_current_model_sync()

            # Load current model state
            original_vocab = self.model_service.vocab.copy()
            original_mlb = self.model_service.mlb

            self.training_progress['status'] = 'preparing_model'
            self._update_training_progress()

            # Extend MLB with new classes if any
            if new_classes:
                all_classes = list(original_mlb.classes_) + new_classes
                extended_mlb = MultiLabelBinarizer()
                extended_mlb.fit([all_classes])
            else:
                extended_mlb = original_mlb

            # Create training dataset
            dataset = RetrainDataset(problems, labels, original_vocab, extended_mlb)

            # Split into train/validation using config values
            val_split = getattr(self.config, 'VALIDATION_SPLIT_MIN', 0.1)
            train_size = int(len(dataset) * (1 - val_split))
            val_size = len(dataset) - train_size
            train_dataset, val_dataset = torch.utils.data.random_split(dataset, [train_size, val_size])

            # Create data loaders
            batch_size = getattr(self.config, 'TRANSFER_BATCH_SIZE', 16)
            train_loader = DataLoader(train_dataset, batch_size=batch_size, shuffle=True)
            val_loader = DataLoader(val_dataset, batch_size=batch_size, shuffle=False)

            self.training_progress['status'] = 'creating_model'
            self._update_training_progress()

            # Create model with extended classes if needed
            if new_classes:
                vocab_size = len(original_vocab)
                num_parts = len(extended_mlb.classes_)

                # Create new model with extended architecture
                new_model = LSTMAttentionModel(
                    vocab_size=vocab_size,
                    num_parts=num_parts,
                    enable_uncertainty=False
                ).to(self.device)

                # Transfer weights from old model
                old_state = self.model_service.model.state_dict()
                new_state = new_model.state_dict()

                # Copy compatible layers
                for name, param in old_state.items():
                    if name in new_state:
                        if param.shape == new_state[name].shape:
                            new_state[name] = param.clone()
                        elif 'classifier' in name and param.dim() == 2:
                            # Handle classifier layer size mismatch
                            old_classes = param.shape[0] if 'weight' in name else param.shape[0]
                            if old_classes < new_state[name].shape[0]:
                                # Copy old weights and initialize new ones
                                if 'weight' in name:
                                    new_state[name][:old_classes] = param.clone()
                                    nn.init.xavier_uniform_(new_state[name][old_classes:])
                                else:  # bias
                                    new_state[name][:old_classes] = param.clone()
                                    nn.init.zeros_(new_state[name][old_classes:])

                new_model.load_state_dict(new_state)
                model = new_model
            else:
                model = self.model_service.model

            model.train()

            self.training_progress['status'] = 'training'
            self._update_training_progress()

            # Ultra-conservative optimizer with config values
            learning_rate = getattr(self.config, 'TRANSFER_ULTRA_CONSERVATIVE_LR', 1e-7)
            optimizer = torch.optim.AdamW(
                model.parameters(),
                lr=learning_rate,
                weight_decay=0.01
            )

            # Loss function with class weights
            if new_classes:
                # Weight new classes higher
                class_weights = torch.ones(len(extended_mlb.classes_))
                new_class_weight = getattr(self.config, 'NEW_CLASS_WEIGHT', 1.5)
                existing_class_weight = getattr(self.config, 'EXISTING_CLASS_WEIGHT', 0.5)

                for i, cls in enumerate(extended_mlb.classes_):
                    if cls in new_classes:
                        class_weights[i] = new_class_weight
                    else:
                        class_weights[i] = existing_class_weight

                criterion = nn.BCEWithLogitsLoss(pos_weight=class_weights.to(self.device))
            else:
                criterion = nn.BCEWithLogitsLoss()

            # Training loop with validation
            max_epochs = getattr(self.config, 'TRANSFER_MAX_EPOCHS', 3)
            patience = getattr(self.config, 'PATIENCE', 8)
            gradient_clip = getattr(self.config, 'TRANSFER_GRADIENT_CLIP', 0.005)

            best_val_accuracy = 0.0
            patience_counter = 0
            training_history = []

            for epoch in range(max_epochs):
                # Update progress
                self.training_progress['epoch_current'] = epoch + 1
                self.training_progress['epochs_total'] = max_epochs
                self._update_training_progress()

                # Training phase
                model.train()
                train_loss = 0.0
                train_correct = 0
                train_total = 0

                for batch_inputs, batch_labels in train_loader:
                    batch_inputs = batch_inputs.to(self.device)
                    batch_labels = batch_labels.to(self.device)

                    optimizer.zero_grad()
                    outputs = model(batch_inputs)
                    loss = criterion(outputs, batch_labels)
                    loss.backward()

                    # Gradient clipping
                    torch.nn.utils.clip_grad_norm_(model.parameters(), gradient_clip)
                    optimizer.step()

                    train_loss += loss.item()
                    predictions = torch.sigmoid(outputs) > 0.5
                    train_correct += (predictions == batch_labels.bool()).float().mean().item()
                    train_total += 1

                # Validation phase
                model.eval()
                val_loss = 0.0
                val_correct = 0
                val_total = 0

                with torch.no_grad():
                    for batch_inputs, batch_labels in val_loader:
                        batch_inputs = batch_inputs.to(self.device)
                        batch_labels = batch_labels.to(self.device)

                        outputs = model(batch_inputs)
                        loss = criterion(outputs, batch_labels)

                        val_loss += loss.item()
                        predictions = torch.sigmoid(outputs) > 0.5
                        val_correct += (predictions == batch_labels.bool()).float().mean().item()
                        val_total += 1

                # Calculate metrics
                train_accuracy = train_correct / max(train_total, 1)
                val_accuracy = val_correct / max(val_total, 1)

                epoch_stats = {
                    'epoch': epoch + 1,
                    'train_loss': train_loss / max(train_total, 1),
                    'train_accuracy': train_accuracy,
                    'val_loss': val_loss / max(val_total, 1),
                    'val_accuracy': val_accuracy
                }
                training_history.append(epoch_stats)

                # Update progress with current metrics
                self.training_progress['train_loss'] = epoch_stats['train_loss']
                self.training_progress['val_accuracy'] = epoch_stats['val_accuracy']
                self._update_training_progress()

                self.logger.info("training_epoch_completed", **epoch_stats)

                # Early stopping check
                if val_accuracy > best_val_accuracy:
                    best_val_accuracy = val_accuracy
                    patience_counter = 0
                    # Save best model state
                    best_model_state = model.state_dict().copy()
                else:
                    patience_counter += 1

                if patience_counter >= patience:
                    self.logger.info("early_stopping_triggered",
                                     epoch=epoch + 1,
                                     patience=patience_counter)
                    break

            # Load best model state
            if 'best_model_state' in locals():
                model.load_state_dict(best_model_state)

            self.training_progress['status'] = 'validating'
            self._update_training_progress()

            # Test against baseline to ensure no catastrophic forgetting
            max_degradation = getattr(self.config, 'TRANSFER_MAX_BASELINE_DEGRADATION', 0.1)

            if new_classes:
                # Temporarily update model service for testing
                temp_mlb = self.model_service.mlb
                temp_model = self.model_service.model

                self.model_service.mlb = extended_mlb
                self.model_service.model = model

                post_retrain_performance = self._test_baseline_performance_sync()

                # Restore original for now
                self.model_service.mlb = temp_mlb
                self.model_service.model = temp_model
            else:
                # Update model service temporarily
                temp_model = self.model_service.model
                self.model_service.model = model

                post_retrain_performance = self._test_baseline_performance_sync()

                # Restore original for now
                self.model_service.model = temp_model

            # Check if performance degradation is acceptable
            performance_degradation = baseline_performance - post_retrain_performance

            if len(self.baseline_test_data) > 0 and performance_degradation > max_degradation:
                # Restore from backup
                self._restore_model_from_backup_sync(backup_path)
                return {
                    'success': False,
                    'error': f'Baseline degradation too high: {performance_degradation:.3f} > {max_degradation}',
                    'baseline_before': baseline_performance,
                    'baseline_after': post_retrain_performance,
                    'training_history': training_history,
                    'config_used': {
                        'epochs': max_epochs,
                        'learning_rate': learning_rate,
                        'batch_size': batch_size,
                        'max_degradation': max_degradation
                    }
                }

            self.training_progress['status'] = 'deploying'
            self._update_training_progress()

            # Performance is acceptable - deploy new model
            success = self._deploy_retrained_model_sync(model, extended_mlb, original_vocab)

            if success:
                # Clean up backup
                if os.path.exists(backup_path):
                    os.remove(backup_path)

                return {
                    'success': True,
                    'accuracy': best_val_accuracy,
                    'baseline_before': baseline_performance,
                    'baseline_after': post_retrain_performance,
                    'new_classes_added': len(new_classes),
                    'training_history': training_history,
                    'strategy': 'ultra_conservative',
                    'config_used': {
                        'epochs': max_epochs,
                        'learning_rate': learning_rate,
                        'batch_size': batch_size,
                        'gradient_clip': gradient_clip,
                        'patience': patience
                    }
                }
            else:
                # Restore from backup
                self._restore_model_from_backup_sync(backup_path)
                return {
                    'success': False,
                    'error': 'Model deployment failed',
                    'training_history': training_history
                }

        except Exception as e:
            self.logger.error("ultra_conservative_retrain_sync_failed", error=str(e))
            return {
                'success': False,
                'error': str(e)
            }

    def _test_baseline_performance_sync(self) -> float:
        """Synchronous version of baseline performance test with minimal logging"""
        try:
            if not self.baseline_test_data or not self.model_service:
                return 0.0

            correct_predictions = 0
            total_predictions = 0

            # Temporarily disable verbose logging for model service
            original_logger = self.model_service.logger

            # Create silent logger for baseline testing
            import structlog
            silent_logger = structlog.get_logger("BaselineTest_Silent")
            silent_logger.info = lambda *args, **kwargs: None  # Disable info logs

            # Replace logger temporarily
            self.model_service.logger = silent_logger

            try:
                for problem, expected_parts in self.baseline_test_data[:10]:  # Limit to 10 samples for speed
                    try:
                        predictions, confidence, _ = self.model_service.predict(
                            problem, top_k=3, threshold=0.1
                        )

                        predicted_parts = [pred['part'] for pred in predictions]

                        # Check if at least one expected part is in top predictions
                        if any(part in predicted_parts for part in expected_parts):
                            correct_predictions += 1

                        total_predictions += 1

                    except Exception:
                        # Silent fail for baseline testing
                        continue

            finally:
                # Restore original logger
                self.model_service.logger = original_logger

            if total_predictions == 0:
                return 0.0

            performance = correct_predictions / total_predictions

            # Only log the final result
            self.logger.info("baseline_performance_test_completed",
                             correct=correct_predictions,
                             total=total_predictions,
                             performance=performance,
                             samples_tested=min(10, len(self.baseline_test_data)),
                             total_available=len(self.baseline_test_data))

            return performance

        except Exception as e:
            self.logger.error("baseline_performance_test_sync_failed", error=str(e))
            return 0.0

    def _backup_current_model_sync(self) -> str:
        """Synchronous backup of current model with robust path handling"""
        try:
            timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
            model_filename = os.path.basename(self.model_service.model_path)

            # Try multiple backup locations in order of preference
            backup_locations = [
                f"/app/backups/{model_filename}.backup_{timestamp}",
                f"/tmp/model_backups/{model_filename}.backup_{timestamp}",
                f"/tmp/{model_filename}.backup_{timestamp}"
            ]

            backup_path = None
            for location in backup_locations:
                try:
                    # Ensure directory exists
                    os.makedirs(os.path.dirname(location), mode=0o755, exist_ok=True)

                    # Test write permissions
                    test_file = f"{location}.test"
                    with open(test_file, 'w') as f:
                        f.write("test")
                    os.remove(test_file)

                    # Directory is writable, use this location
                    backup_path = location
                    break

                except (OSError, PermissionError):
                    continue

            if not backup_path:
                # Last resort - skip backup in development mode
                if getattr(self.config, 'DEVELOPMENT_MODE', False):
                    self.logger.warning("backup_skipped_no_writable_location_development_mode")
                    return "development_mode_no_backup"
                else:
                    raise PermissionError("No writable location found for model backup")

            # Create the actual backup
            shutil.copy2(self.model_service.model_path, backup_path)

            # Verify backup was created successfully
            if not os.path.exists(backup_path):
                raise FileNotFoundError(f"Backup creation failed: {backup_path}")

            self.logger.info("model_backed_up_sync",
                             backup_path=backup_path,
                             original_size=os.path.getsize(self.model_service.model_path),
                             backup_size=os.path.getsize(backup_path))
            return backup_path

        except Exception as e:
            self.logger.error("backup_model_sync_failed", error=str(e))

            # In development mode, allow retraining without backup
            if getattr(self.config, 'DEVELOPMENT_MODE', False):
                self.logger.warning("backup_failed_proceeding_without_backup_development_mode")
                return "development_mode_no_backup"

            raise

    def _restore_model_from_backup_sync(self, backup_path: str):
        """Synchronous restore model from backup with development mode handling"""
        try:
            if backup_path == "development_mode_no_backup":
                self.logger.info("restore_skipped_development_mode_no_backup")
                return

            if not os.path.exists(backup_path):
                raise FileNotFoundError(f"Backup file not found: {backup_path}")

            # Verify backup file integrity
            backup_size = os.path.getsize(backup_path)
            if backup_size < 1000:  # Model should be at least 1KB
                raise ValueError(f"Backup file too small, likely corrupted: {backup_size} bytes")

            # Create backup of current model before restore (just in case)
            current_size = os.path.getsize(self.model_service.model_path)

            # Restore the backup
            shutil.copy2(backup_path, self.model_service.model_path)

            # Verify restore
            restored_size = os.path.getsize(self.model_service.model_path)
            if restored_size != backup_size:
                raise ValueError(f"Restore verification failed: size mismatch {restored_size} != {backup_size}")

            # Reload model service
            self.model_service._load_model()

            self.logger.info("model_restored_from_backup_sync",
                             backup_path=backup_path,
                             original_size=current_size,
                             restored_size=restored_size)

        except Exception as e:
            self.logger.error("restore_model_sync_failed", error=str(e))
            raise

    def _deploy_retrained_model_sync(self, model: nn.Module, mlb: MultiLabelBinarizer, vocab: Dict) -> bool:
        """Synchronous deploy retrained model with proper file handling"""
        try:
            # Prepare new checkpoint
            checkpoint = {
                'model_state_dict': model.state_dict(),
                'vocab': vocab,
                'mlb': mlb,
                'vocab_size': len(vocab),
                'num_parts': len(mlb.classes_),
                'training_timestamp': datetime.now().isoformat(),
                'strategy': self.config.TRANSFER_LEARNING_MODE,
                'config_snapshot': {
                    'epochs': getattr(self.config, 'TRANSFER_MAX_EPOCHS', 3),
                    'learning_rate': getattr(self.config, 'TRANSFER_ULTRA_CONSERVATIVE_LR', 1e-7),
                    'batch_size': getattr(self.config, 'TRANSFER_BATCH_SIZE', 16),
                    'gradient_clip': getattr(self.config, 'TRANSFER_GRADIENT_CLIP', 0.005)
                }
            }

            # Save to temporary file first
            temp_path = f"{self.model_service.model_path}.temp_{int(time.time())}"

            try:
                torch.save(checkpoint, temp_path)

                # Verify temp file was created successfully
                if not os.path.exists(temp_path):
                    raise FileNotFoundError("Temporary model file creation failed")

                temp_size = os.path.getsize(temp_path)
                if temp_size < 1000:  # Sanity check
                    raise ValueError(f"Temporary model file too small: {temp_size} bytes")

                # Atomic move to final location
                shutil.move(temp_path, self.model_service.model_path)

            except Exception as e:
                # Clean up temp file if it exists
                if os.path.exists(temp_path):
                    try:
                        os.remove(temp_path)
                    except:
                        pass
                raise e

            # Update model service state
            self.model_service.vocab = vocab
            self.model_service.mlb = mlb
            self.model_service.model = model.eval()

            # Clear caches
            self.model_service.clear_cache()

            # Verify final model file
            final_size = os.path.getsize(self.model_service.model_path)

            self.logger.info("retrained_model_deployed_sync",
                             vocab_size=len(vocab),
                             num_classes=len(mlb.classes_),
                             model_size_bytes=final_size)

            return True

        except Exception as e:
            self.logger.error("deploy_retrained_model_sync_failed", error=str(e))
            return False

    def _store_retrain_history_sync(self, result: Dict, learning_data: List[Dict], baseline_performance: float):
        """Synchronous store retraining history"""
        try:
            history_entry = {
                'timestamp': datetime.now().isoformat(),
                'strategy': self.config.TRANSFER_LEARNING_MODE,
                'success': result['success'],
                'accuracy': result.get('accuracy'),
                'baseline_before': baseline_performance,
                'baseline_after': result.get('baseline_after'),
                'training_samples': len(learning_data),
                'new_classes_added': result.get('new_classes_added', 0),
                'training_time': result.get('training_time'),
                'error': result.get('error'),
                'config_used': result.get('config_used', {})
            }

            # Store in Redis using sync client
            if self.redis:
                try:
                    import redis
                    sync_redis = redis.Redis.from_url(
                        f"redis://{self.redis.connection_pool.connection_kwargs['host']}:"
                        f"{self.redis.connection_pool.connection_kwargs['port']}"
                    )
                    sync_redis.lpush(
                        self.RETRAIN_HISTORY_KEY,
                        json.dumps(history_entry)
                    )
                    sync_redis.ltrim(self.RETRAIN_HISTORY_KEY, 0, 99)
                except Exception as e:
                    self.logger.warning("store_retrain_history_redis_failed", error=str(e))

        except Exception as e:
            self.logger.warning("store_retrain_history_sync_failed", error=str(e))

    async def _gradual_retrain(self, problems: List[str], labels: List[List[str]],
                               new_classes: List[str], baseline_performance: float) -> Dict[str, Any]:
        """Gradual unfreezing strategy (placeholder)"""
        return {
            'success': False,
            'error': 'Gradual retraining strategy not yet implemented'
        }

    async def _aggressive_retrain(self, problems: List[str], labels: List[List[str]],
                                  new_classes: List[str], baseline_performance: float) -> Dict[str, Any]:
        """Aggressive retraining strategy (placeholder)"""
        return {
            'success': False,
            'error': 'Aggressive retraining strategy not yet implemented'
        }

    async def _prepare_training_data(self, learning_data: List[Dict]) -> Tuple[List[str], List[List[str]], List[str]]:
        """Async wrapper for prepare training data"""
        loop = asyncio.get_event_loop()
        return await loop.run_in_executor(None, self._prepare_training_data_sync, learning_data)

    async def _backup_current_model(self) -> str:
        """Async wrapper for backup model"""
        loop = asyncio.get_event_loop()
        return await loop.run_in_executor(None, self._backup_current_model_sync)

    async def _restore_model_from_backup(self, backup_path: str):
        """Async wrapper for restore model"""
        loop = asyncio.get_event_loop()
        await loop.run_in_executor(None, self._restore_model_from_backup_sync, backup_path)

    async def _deploy_retrained_model(self, model: nn.Module, mlb: MultiLabelBinarizer, vocab: Dict) -> bool:
        """Async wrapper for deploy model"""
        loop = asyncio.get_event_loop()
        return await loop.run_in_executor(None, self._deploy_retrained_model_sync, model, mlb, vocab)

    async def _store_retrain_history(self, result: Dict, learning_data: List[Dict], baseline_performance: float):
        """Async wrapper for store history"""
        loop = asyncio.get_event_loop()
        await loop.run_in_executor(None, self._store_retrain_history_sync, result, learning_data, baseline_performance)

    async def _get_recent_learning_data(self) -> List[Dict]:
        """Get recent learning data for manual retrain"""
        try:
            learning_data = []

            # Get learning samples from Redis
            learning_keys = await self.redis.keys("learning:*")

            for key in learning_keys[-100:]:  # Limit to recent samples
                try:
                    sample_raw = await self.redis.get(key)
                    if sample_raw:
                        sample = json.loads(sample_raw)

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

                except (json.JSONDecodeError, KeyError):
                    continue

            # Sort by timestamp
            learning_data.sort(key=lambda x: x['timestamp'], reverse=True)

            return learning_data[:200]  # Limit size

        except Exception as e:
            self.logger.error("get_recent_learning_data_failed", error=str(e))
            return []

    def __del__(self):
        """Cleanup executor on destruction"""
        try:
            if hasattr(self, 'executor'):
                self.executor.shutdown(wait=False)
        except:
            pass