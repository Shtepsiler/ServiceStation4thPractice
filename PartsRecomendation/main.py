import asyncio
import glob
import hashlib
import json
import os
import time
from contextlib import asynccontextmanager
from datetime import datetime, timedelta
from typing import List, Dict, Any, Optional

import uvicorn
from fastapi import FastAPI, HTTPException, BackgroundTasks, Request
from fastapi.middleware.cors import CORSMiddleware
from slowapi import Limiter, _rate_limit_exceeded_handler
from slowapi.util import get_remote_address
from slowapi.errors import RateLimitExceeded

from CONFIG import Config
from dependencies import container, CacheService, sanitize_logs
from PydanticModels import (
    HealthResponse, PredictionResponse, PredictionRequest,
    FeedbackRequest, ModelStatus, BatchPredictionRequest,
    BatchPredictionResponse, PredictionItem
)


@asynccontextmanager
async def lifespan(app: FastAPI):
    """Application lifespan management"""
    # Startup
    startup_start = time.time()
    container.logger = container._setup_logging()
    container.logger.info("api_starting")

    try:
        await container.initialize()
        container.stats['startup_time'] = time.time() - startup_start
        container.logger.info("api_started", startup_time=container.stats['startup_time'])
    except Exception as e:
        container.logger.error("api_startup_failed", error=str(e))
        container.stats['startup_time'] = time.time() - startup_start

    yield

    # Shutdown
    container.logger.info("api_shutting_down")


# Initialize FastAPI app
app = FastAPI(
    title="Auto Parts Classification API",
    description="AI-powered auto parts classification with active learning",
    version="1.0.0",
    docs_url="/swagger",
    lifespan=lifespan
)

# Rate limiting
limiter = Limiter(key_func=get_remote_address)
app.state.limiter = limiter
app.add_exception_handler(RateLimitExceeded, _rate_limit_exceeded_handler)

# CORS middleware
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)


@app.middleware("http")
async def log_requests_middleware(request: Request, call_next):
    """Enhanced request/response logging with sanitization"""
    start_time = time.time()
    request_id = hashlib.md5(f"{time.time()}".encode()).hexdigest()[:8]

    # Log incoming request
    if container.logger:
        container.logger.info("request_started",
                              request_id=request_id,
                              method=request.method,
                              url=str(request.url.path),
                              client_ip=get_remote_address(request))

    # Handle request body logging for POST/PUT/PATCH
    if request.method in ["POST", "PUT", "PATCH"]:
        body = await request.body()
        if body:
            try:
                body_data = json.loads(body.decode())
                sanitized_data = sanitize_logs(body_data)
                if container.logger:
                    container.logger.info("request_body",
                                          request_id=request_id,
                                          data=sanitized_data)
            except json.JSONDecodeError:
                if container.logger:
                    container.logger.info("request_body_raw",
                                          request_id=request_id,
                                          size=len(body))

            # Recreate request for FastAPI
            async def receive():
                return {"type": "http.request", "body": body}

            request._receive = receive

    response = await call_next(request)

    # Log response
    process_time = time.time() - start_time
    if container.logger:
        container.logger.info("request_completed",
                              request_id=request_id,
                              status_code=response.status_code,
                              processing_time=process_time)

    return response


# =================== API ENDPOINTS ===================

@app.get("/", include_in_schema=False)
async def root():
    """Root endpoint with service status"""
    response = {
        "message": "Auto Parts Classification API",
        "version": "1.0.0",
        "docs": "/swagger",
        "health": "/health",
        "status": {
            "model_loaded": container.model_service is not None,
            "redis_available": container.redis_available,
            "active_learning_available": container.active_learning_service is not None,
            "cache_hit_rate": _calculate_cache_hit_rate()
        }
    }
    return response


@app.get("/health", response_model=HealthResponse)
async def health_check():
    """Enhanced health check with deep status"""
    try:
        redis_connected = False
        if container.redis_client:
            try:
                await container.redis_client.ping()
                redis_connected = True
            except:
                redis_connected = False

        # Determine overall status
        if container.model_service and redis_connected:
            status = "healthy"
        elif container.model_service:
            status = "degraded"
        else:
            status = "unhealthy"

        # Calculate uptime
        uptime = 0
        if container.stats.get('startup_time'):
            uptime = time.time() - (time.time() - container.stats['startup_time'])

        response = HealthResponse(
            status=status,
            ml_model_loaded=container.model_service is not None,
            redis_connected=redis_connected,
            version="1.0.0",
            uptime_seconds=uptime,
            environment={
                "redis_available": container.redis_available,
                "model_loaded": container.model_service is not None,
                "active_learning": container.active_learning_service is not None,
                "cache_hit_rate": _calculate_cache_hit_rate()
            }
        )

        if container.logger:
            container.logger.info("health_check_completed", status=status)
        return response

    except Exception as e:
        if container.logger:
            container.logger.error("health_check_failed", error=str(e))

        return HealthResponse(
            status="unhealthy",
            ml_model_loaded=False,
            redis_connected=False,
            version="1.0.0",
            uptime_seconds=0,
            environment={"error": str(e)}
        )


@app.post("/predict", response_model=PredictionResponse)
@limiter.limit("100/minute")
async def predict_parts(request: Request, prediction_request: PredictionRequest,
                        background_tasks: BackgroundTasks):
    """Predict auto parts with optimized caching, error handling and production active learning"""

    if not container.model_service:
        raise HTTPException(
            status_code=503,
            detail="Model service not available"
        )

    try:
        # Initialize cache service
        cache_service = CacheService(container.redis_client, container.logger, container)

        # Check cache first
        cached_result = await cache_service.get_cached_prediction(
            prediction_request.problem_description
        )

        if cached_result:
            if container.logger:
                container.logger.info("prediction_cache_hit",
                                      prediction_id=cached_result.prediction_id)
            return cached_result

        # Make prediction
        predictions, confidence, processing_time = container.model_service.predict(
            prediction_request.problem_description,
            top_k=prediction_request.top_k,
            threshold=prediction_request.confidence_threshold
        )

        # Generate prediction ID
        prediction_id = hashlib.md5(
            f"{prediction_request.problem_description}_{time.time()}".encode()
        ).hexdigest()[:16]

        # Update stats
        container.stats['total_predictions'] += 1

        # Create detailed predictions
        detailed_predictions = []
        simple_predictions = []

        for pred in predictions:
            pred_item = PredictionItem(
                part_name=pred['part'],
                confidence=pred['confidence'],
                category=None,
                explanation=None,
                estimated_cost=None
            )
            detailed_predictions.append(pred_item)
            simple_predictions.append({pred['part']: pred['confidence']})

        # Create response
        response = PredictionResponse(
            predictions=detailed_predictions,
            simple_predictions=simple_predictions,
            confidence_score=confidence,
            prediction_id=prediction_id,
            processing_time=processing_time,
            ml_model_version=getattr(container.model_service, 'model_version', '1.0.0'),
            timestamp=datetime.now(),
            cached=False
        )

        # Cache the result
        background_tasks.add_task(
            cache_service.set_cached_prediction,
            prediction_request.problem_description,
            response,
            3600
        )

        # Store for active learning (existing)
        if container.active_learning_service:
            background_tasks.add_task(
                container.active_learning_service.store_prediction,
                prediction_id, prediction_request, predictions, confidence
            )


        if container.logger:
            container.logger.info("prediction_completed",
                                  prediction_id=prediction_id,
                                  confidence=confidence,
                                  processing_time=processing_time)

        return response

    except Exception as e:
        if container.logger:
            container.logger.error("prediction_failed", error=str(e))
        raise HTTPException(status_code=500, detail=f"Prediction failed: {str(e)}")

@app.post("/predict/batch", response_model=BatchPredictionResponse)
@limiter.limit("10/minute")
async def batch_predict_parts(request: Request, batch_request: BatchPredictionRequest,
                              background_tasks: BackgroundTasks):  # ← Додай BackgroundTasks
    """Batch prediction endpoint for efficiency"""

    if not container.model_service:
        raise HTTPException(status_code=503, detail="Model service not available")

    try:
        start_time = time.time()
        batch_id = hashlib.md5(f"batch_{time.time()}".encode()).hexdigest()[:16]

        results = []

        # Process batch
        batch_results = await container.model_service.batch_predict(
            batch_request.problems,
            top_k=5,
            threshold=0.1
        )

        for i, (predictions, confidence, proc_time) in enumerate(batch_results):
            prediction_id = f"{batch_id}_{i:03d}"

            #  Створи individual request для active learning
            individual_request = PredictionRequest(
                problem_description=batch_request.problems[i],
                user_id=batch_request.user_id,
                language="uk",
                include_explanations=False,
                top_k=5,
                confidence_threshold=0.1
            )

            # Create detailed predictions
            detailed_predictions = [
                PredictionItem(
                    part_name=pred['part'],
                    confidence=pred['confidence'],
                    category=None,
                    explanation=None,
                    estimated_cost=None
                ) for pred in predictions
            ]

            simple_predictions = [
                {pred['part']: pred['confidence']} for pred in predictions
            ]

            response = PredictionResponse(
                predictions=detailed_predictions,
                simple_predictions=simple_predictions,
                confidence_score=confidence,
                prediction_id=prediction_id,
                processing_time=proc_time,
                ml_model_version=getattr(container.model_service, 'model_version', '1.0.0'),
                timestamp=datetime.now(),
                cached=False
            )

            results.append(response)

            # Store for active learning з правильним request
            if container.active_learning_service:
                background_tasks.add_task(
                    container.active_learning_service.store_prediction,
                    prediction_id,
                    individual_request,  # ← Правильний тип
                    predictions,
                    confidence
                )

        total_time = time.time() - start_time
        container.stats['total_predictions'] += len(results)

        batch_response = BatchPredictionResponse(
            results=results,
            batch_id=batch_id,
            total_processed=len(results),
            total_processing_time=total_time,
            timestamp=datetime.now()
        )

        if container.logger:
            container.logger.info("batch_prediction_completed",
                                  batch_id=batch_id,
                                  count=len(results),
                                  total_time=total_time)

        return batch_response

    except Exception as e:
        if container.logger:
            container.logger.error("batch_prediction_failed", error=str(e))
        raise HTTPException(status_code=500, detail=f"Batch prediction failed: {str(e)}")


@app.post("/feedback")
@limiter.limit("200/minute")
async def submit_feedback(request: Request, feedback: FeedbackRequest,
                          background_tasks: BackgroundTasks):
    """Submit feedback for active learning"""

    try:
        if container.active_learning_service:
            background_tasks.add_task(
                container.active_learning_service.store_feedback, feedback
            )
            container.stats['feedback_count'] += 1
            message = "Feedback received and queued for processing"
        else:
            message = "Feedback received (active learning unavailable)"

        response = {
            "message": message,
            "feedback_id": feedback.prediction_id,
            "timestamp": datetime.now().isoformat()
        }

        if container.logger:
            container.logger.info("feedback_received",
                                  prediction_id=feedback.prediction_id,
                                  rating=feedback.user_rating)

        return response

    except Exception as e:
        if container.logger:
            container.logger.error("feedback_failed", error=str(e))
        raise HTTPException(status_code=500, detail=f"Feedback submission failed: {str(e)}")

@app.post("/feedback/batch")
@limiter.limit("50/minute")
async def submit_feedback_batch(request: Request, feedbacks: List[FeedbackRequest],
                              background_tasks: BackgroundTasks):
   """Submit multiple feedback entries for active learning"""

   try:
       for feedback in feedbacks:
           if container.active_learning_service:
               background_tasks.add_task(
                   container.active_learning_service.store_feedback, feedback
               )
               container.stats['feedback_count'] += 1

       if container.active_learning_service:
           message = "Feedback received and queued for processing"
       else:
           message = "Feedback received (active learning unavailable)"

       response = {
           "message": message,
           "feedback_count": len(feedbacks),
           "timestamp": datetime.now().isoformat()
       }

       if container.logger:
           for feedback in feedbacks:
               container.logger.info("feedback_received",
                                     prediction_id=feedback.prediction_id,
                                     rating=feedback.user_rating)

       return response

   except Exception as e:
       if container.logger:
           container.logger.error("feedback_failed", error=str(e))
       raise HTTPException(status_code=500, detail=f"Feedback submission failed: {str(e)}")
@app.get("/uncertain-samples")
@limiter.limit("50/minute")
async def get_uncertain_samples(request: Request, limit: int = 10):
    """Get uncertain predictions for human labeling"""

    if not container.active_learning_service:
        return {
            "message": "Active learning service not available",
            "samples": [],
            "count": 0
        }

    try:
        samples = await container.active_learning_service.get_uncertain_samples(limit)
        return {"samples": samples, "count": len(samples)}

    except Exception as e:
        if container.logger:
            container.logger.error("uncertain_samples_failed", error=str(e))
        raise HTTPException(status_code=500, detail=str(e))


@app.get("/status", response_model=ModelStatus)
async def get_model_status():
    """Get model and service status"""

    try:
        # Get active learning status if available
        al_status = {}
        if container.active_learning_service:
            al_status = await container.active_learning_service.get_health_status()

        response = ModelStatus(
            ml_model_version=getattr(container.model_service, 'model_version', 'unknown')
            if container.model_service else "unknown",
            accuracy=container.stats['accuracy'],
            total_predictions=container.stats['total_predictions'],
            feedback_count=container.stats['feedback_count'],
            last_retrain=datetime.fromisoformat(al_status.get('last_retrain'))
            if al_status.get('last_retrain') else None,
            active_learning_queue=al_status.get('uncertainty_queue_size', 0)
        )

        return response

    except Exception as e:
        if container.logger:
            container.logger.error("status_failed", error=str(e))
        raise HTTPException(status_code=500, detail=str(e))


@app.get("/metrics")
async def get_metrics():
    """Get detailed metrics for monitoring"""

    cache_hit_rate = _calculate_cache_hit_rate()

    response = {
        "predictions_total": container.stats['total_predictions'],
        "feedback_total": container.stats['feedback_count'],
        "model_accuracy": container.stats['accuracy'],
        "feedback_ratio": container.stats['feedback_count'] / max(container.stats['total_predictions'], 1),
        "cache_hit_rate": cache_hit_rate,
        "cache_hits": container.stats.get('cache_hits', 0),
        "cache_misses": container.stats.get('cache_misses', 0),
        "redis_available": container.redis_available,
        "model_loaded": container.model_service is not None,
        "active_learning_available": container.active_learning_service is not None,
        "uptime_seconds": time.time() - (time.time() - container.stats['startup_time'])
        if container.stats.get('startup_time') else 0,
        "timestamp": datetime.now().isoformat()
    }

    return response

@app.get("/admin/retrain/status")
async def get_retrain_status():
    """Get retraining status and eligibility"""
    if not container.retrain_service:
        return {"error": "Retrain service not available"}

    try:
        eligibility = await container.retrain_service.check_retrain_eligibility()

        # Get additional stats
        stats = {}
        if container.active_learning_service:
            stats = await container.active_learning_service.get_learning_statistics()

        return {
            "retrain_eligibility": eligibility,
            "learning_statistics": stats,
            "timestamp": datetime.now().isoformat()
        }

    except Exception as e:
        if container.logger:
            container.logger.error("retrain_status_failed", error=str(e))
        raise HTTPException(status_code=500, detail=str(e))

@app.post("/admin/retrain/trigger")
async def trigger_manual_retrain():
    """Manually trigger model retraining"""
    if not container.retrain_service:
        raise HTTPException(status_code=503, detail="Retrain service not available")

    try:
        result = await container.retrain_service.trigger_retrain()

        if container.logger:
            container.logger.info("manual_retrain_triggered", result=result)

        return result

    except Exception as e:
        if container.logger:
            container.logger.error("manual_retrain_failed", error=str(e))
        raise HTTPException(status_code=500, detail=str(e))


@app.get("/admin/learning/statistics")
async def get_learning_statistics():
    """Get detailed active learning statistics"""
    if not container.active_learning_service:
        return {"error": "Active learning service not available"}

    try:
        stats = await container.active_learning_service.get_learning_statistics()

        # Add model info
        model_info = {}
        if container.model_service:
            model_info = container.model_service.get_model_info()

        return {
            "active_learning": stats,
            "model_info": model_info,
            "timestamp": datetime.now().isoformat()
        }

    except Exception as e:
        if container.logger:
            container.logger.error("learning_statistics_failed", error=str(e))
        raise HTTPException(status_code=500, detail=str(e))
@app.get("/admin/retrain/progress")
async def get_training_progress():
    """Get real-time training progress"""
    if not container.retrain_service:
        return {"error": "Retrain service not available"}

    try:
        progress = await container.retrain_service.get_training_progress()
        return progress
    except Exception as e:
        if container.logger:
            container.logger.error("training_progress_failed", error=str(e))
        raise HTTPException(status_code=500, detail=str(e))

@app.post("/admin/clear-cache")
async def clear_cache():
    """Clear model and Redis cache"""
    try:
        cleared = {"model_cache": False, "redis_cache": False}

        if container.model_service:
            container.model_service.clear_cache()
            cleared["model_cache"] = True

        if container.redis_client:
            await container.redis_client.flushdb()
            cleared["redis_cache"] = True

        # Reset cache stats
        container.stats['cache_hits'] = 0
        container.stats['cache_misses'] = 0

        if container.logger:
            container.logger.info("cache_cleared", cleared=cleared)

        return {"message": "Cache cleared", "cleared": cleared}

    except Exception as e:
        if container.logger:
            container.logger.error("cache_clear_failed", error=str(e))
        raise HTTPException(status_code=500, detail=str(e))


# =================== HELPER FUNCTIONS ===================

def _calculate_cache_hit_rate() -> float:
    """Calculate cache hit rate"""
    total_requests = container.stats.get('cache_hits', 0) + container.stats.get('cache_misses', 0)
    if total_requests == 0:
        return 0.0
    return container.stats.get('cache_hits', 0) / total_requests


if __name__ == "__main__":
    uvicorn.run(
        "main:app",
        host="0.0.0.0",
        port=8000,
        reload=False,
        workers=1,
        log_level="info"
    )