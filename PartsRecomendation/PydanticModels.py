from typing import Dict, List, Optional, Any
from datetime import datetime
from pydantic import BaseModel, Field, ConfigDict, validator
import re


# Base model with fixed namespace issues
class BaseModelConfig(BaseModel):
    model_config = ConfigDict(
        protected_namespaces=(),  # Fix for pydantic warnings
        str_strip_whitespace=True,  # Auto strip whitespace
        validate_assignment=True,  # Validate on assignment
        extra='forbid'  # Forbid extra fields
    )


class PredictionRequest(BaseModelConfig):
    problem_description: str = Field(
        ...,
        min_length=10,
        max_length=1000,
        description="Vehicle problem description in Ukrainian or English",
        examples=["двигун троїть на холостих оборотах", "engine misfiring at idle"]
    )
    user_id: Optional[str] = Field(
        None,
        max_length=50,
        description="User identifier for tracking and personalization"
    )
    language: Optional[str] = Field(
        default="uk",
        pattern="^(uk|en|auto)$",
        description="Language of the problem description: uk, en, or auto-detect"
    )
    include_explanations: bool = Field(
        default=False,
        description="Include explanations for predictions"
    )
    top_k: int = Field(
        default=5,
        ge=1,
        le=10,
        description="Number of top predictions to return"
    )
    confidence_threshold: float = Field(
        default=0.5,
        ge=0.0,
        le=1.0,
        description="Minimum confidence threshold for predictions"
    )

    @validator('problem_description')
    def validate_problem_description(cls, v):
        """Validate problem description content"""
        if not v or v.isspace():
            raise ValueError('Problem description cannot be empty or whitespace only')

        # Check for meaningful content (not just numbers/symbols)
        if len(re.sub(r'[^a-zA-Zа-яіїєґА-ЯІЇЄҐ]', '', v)) < 5:
            raise ValueError('Problem description must contain meaningful text')

        return v.strip()


class PredictionItem(BaseModelConfig):
    part_name: str = Field(..., description="Name of the automotive part")
    confidence: float = Field(..., ge=0.0, le=1.0, description="Confidence score (0-1)")
    category: Optional[str] = Field(None, description="Part category (engine, brake, etc.)")
    explanation: Optional[str] = Field(None, description="Why this part was predicted")
    estimated_cost: Optional[float] = Field(None, ge=0, description="Estimated replacement cost")


class PredictionResponse(BaseModelConfig):
    predictions: List[PredictionItem] = Field(
        ...,
        min_items=1,
        max_items=10,
        description="List of predicted parts with details"
    )
    # Alternative format for backward compatibility
    simple_predictions: List[Dict[str, float]] = Field(
        ...,
        description="Simple format: list of {part: confidence}"
    )
    confidence_score: float = Field(
        ...,
        ge=0.0,
        le=1.0,
        description="Overall prediction confidence"
    )
    prediction_id: str = Field(
        ...,
        min_length=8,
        max_length=32,
        description="Unique prediction identifier"
    )
    processing_time: float = Field(
        ...,
        ge=0.0,
        description="Processing time in seconds"
    )
    ml_model_version: str = Field(
        ...,
        description="Version of the ML model used"
    )
    timestamp: datetime = Field(
        default_factory=datetime.now,
        description="When the prediction was made"
    )
    cached: bool = Field(
        default=False,
        description="Whether result came from cache"
    )
    language_detected: Optional[str] = Field(
        None,
        description="Auto-detected language if language='auto'"
    )


class FeedbackRequest(BaseModelConfig):
    prediction_id: str = Field(
        ...,
        min_length=8,
        max_length=32,
        description="Prediction ID from original request"
    )
    correct_parts: List[str] = Field(
        ...,
        min_items=1,
        max_items=20,
        description="List of correct part names"
    )
    user_rating: Optional[int] = Field(
        None,
        ge=1,
        le=5,
        description="User satisfaction rating (1-5 stars)"
    )
    comments: Optional[str] = Field(
        None,
        max_length=500,
        description="Additional feedback comments"
    )
    user_id: Optional[str] = Field(
        None,
        max_length=50,
        description="User identifier"
    )
    is_correct_prediction: bool = Field(
        ...,
        description="Whether the top prediction was correct"
    )
    time_to_feedback: Optional[float] = Field(
        None,
        ge=0.0,
        description="Time from prediction to feedback (seconds)"
    )

    @validator('correct_parts')
    def validate_correct_parts(cls, v):
        """Validate correct parts list"""
        if not v:
            raise ValueError('At least one correct part must be specified')

        # Remove duplicates while preserving order
        seen = set()
        unique_parts = []
        for part in v:
            if part.strip() and part.strip() not in seen:
                seen.add(part.strip())
                unique_parts.append(part.strip())

        if not unique_parts:
            raise ValueError('No valid parts provided after cleaning')

        return unique_parts


class ModelStatus(BaseModelConfig):
    ml_model_version: str = Field(
        ...,
        description="Current ML model version"
    )
    accuracy: float = Field(
        ...,
        ge=0.0,
        le=1.0,
        description="Current model accuracy (0-1)"
    )
    total_predictions: int = Field(
        ...,
        ge=0,
        description="Total predictions made since startup"
    )
    feedback_count: int = Field(
        ...,
        ge=0,
        description="Total feedback received"
    )
    last_retrain: Optional[datetime] = Field(
        None,
        description="Timestamp of last model retraining"
    )
    active_learning_queue: int = Field(
        ...,
        ge=0,
        description="Number of samples in active learning queue"
    )
    model_file_size: Optional[int] = Field(
        None,
        ge=0,
        description="Model file size in bytes"
    )
    training_samples: Optional[int] = Field(
        None,
        ge=0,
        description="Number of samples model was trained on"
    )
    classes_count: Optional[int] = Field(
        None,
        ge=0,
        description="Number of part classes the model can predict"
    )


class HealthResponse(BaseModelConfig):
    status: str = Field(
        ...,
        pattern="^(healthy|degraded|unhealthy)$",
        description="Service health status"
    )
    ml_model_loaded: bool = Field(
        ...,
        description="Whether ML model is loaded and ready"
    )
    redis_connected: bool = Field(
        ...,
        description="Whether Redis cache is connected"
    )
    version: str = Field(
        ...,
        description="API version"
    )
    uptime_seconds: Optional[float] = Field(
        None,
        ge=0.0,
        description="Service uptime in seconds"
    )
    environment: Optional[Dict[str, Any]] = Field(
        None,
        description="Environment information (for debugging)"
    )
    memory_usage: Optional[Dict[str, float]] = Field(
        None,
        description="Memory usage statistics"
    )
    gpu_available: Optional[bool] = Field(
        None,
        description="Whether GPU is available for inference"
    )


class ErrorResponse(BaseModelConfig):
    error: str = Field(..., description="Error message")
    error_code: Optional[str] = Field(None, description="Machine-readable error code")
    details: Optional[Dict[str, Any]] = Field(None, description="Additional error details")
    timestamp: datetime = Field(default_factory=datetime.now)
    request_id: Optional[str] = Field(None, description="Request ID for tracking")


class MetricsResponse(BaseModelConfig):
    predictions_total: int = Field(..., ge=0)
    feedback_total: int = Field(..., ge=0)
    accuracy_current: float = Field(..., ge=0.0, le=1.0)
    feedback_ratio: float = Field(..., ge=0.0, description="Feedback/Predictions ratio")
    average_confidence: float = Field(..., ge=0.0, le=1.0)
    average_processing_time: float = Field(..., ge=0.0, description="Average processing time in seconds")
    cache_hit_rate: float = Field(..., ge=0.0, le=1.0, description="Cache hit rate")
    active_learning_enabled: bool = Field(...)
    ml_model_performance: Dict[str, float] = Field(
        ...,
        description="Detailed model performance metrics"
    )
    system_resources: Dict[str, float] = Field(
        ...,
        description="System resource usage"
    )
    timestamp: datetime = Field(default_factory=datetime.now)


class BatchPredictionRequest(BaseModelConfig):
    problems: List[str] = Field(
        ...,
        min_items=1,
        max_items=100,
        description="List of problem descriptions to process"
    )
    user_id: Optional[str] = Field(None)
    include_individual_ids: bool = Field(
        default=True,
        description="Include individual prediction IDs"
    )

    @validator('problems')
    def validate_problems(cls, v):
        """Validate batch problems"""
        if not v:
            raise ValueError('At least one problem must be provided')

        valid_problems = []
        for i, problem in enumerate(v):
            if isinstance(problem, str) and len(problem.strip()) >= 10:
                valid_problems.append(problem.strip())
            else:
                raise ValueError(f'Problem {i + 1} is too short or invalid')

        return valid_problems


class BatchPredictionResponse(BaseModelConfig):
    results: List[PredictionResponse] = Field(
        ...,
        description="Individual prediction results"
    )
    batch_id: str = Field(
        ...,
        description="Unique batch identifier"
    )
    total_processed: int = Field(..., ge=0)
    total_processing_time: float = Field(..., ge=0.0)
    timestamp: datetime = Field(default_factory=datetime.now)


# Configuration for different environments
class APIConfig(BaseModelConfig):
    redis_enabled: bool = Field(default=True)
    cache_ttl: int = Field(default=3600, ge=0)
    max_predictions_per_minute: int = Field(default=1000, ge=1)
    enable_active_learning: bool = Field(default=True)
    ml_model_path: str = Field(...)
    log_level: str = Field(default="INFO", pattern="^(DEBUG|INFO|WARNING|ERROR|CRITICAL)$")
    max_concurrent_requests: int = Field(default=10, ge=1)


# Export all models for easy import
__all__ = [
    'PredictionRequest',
    'PredictionResponse',
    'PredictionItem',
    'FeedbackRequest',
    'ModelStatus',
    'HealthResponse',
    'ErrorResponse',
    'MetricsResponse',
    'BatchPredictionRequest',
    'BatchPredictionResponse',
    'APIConfig'
]