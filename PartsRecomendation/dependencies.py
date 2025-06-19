import asyncio
import json
import logging
from functools import lru_cache
from typing import Optional, Dict, Any
from datetime import datetime
import redis.asyncio as aioredis
import structlog
from pydantic import ValidationError

from PydanticModels import PredictionResponse


class ServiceContainer:
    """Dependency injection container"""

    def __init__(self):
        self.model_service = None
        self.active_learning_service = None
        self.retrain_service = None
        self.redis_client = None
        self.redis_available = False
        self.logger = None
        self.stats = {
            'total_predictions': 0,
            'feedback_count': 0,
            'accuracy': 0.9515,
            'startup_time': None,
            'cache_hits': 0,
            'cache_misses': 0
        }

    async def initialize(self):
        """Initialize all services"""
        self.logger = self._setup_logging()
        await self._initialize_redis()
        await self._initialize_model_service()
        await self._initialize_active_learning()

    def _setup_logging(self):
        """Setup structured logging with proper logger names"""
        import logging
        import sys

        # Налаштовуємо базовий Python logger
        logging.basicConfig(
            level=logging.INFO,
            format='%(asctime)s %(name)s %(levelname)s: %(message)s',
            handlers=[logging.StreamHandler(sys.stdout)]
        )

        structlog.configure(
            processors=[
                structlog.stdlib.filter_by_level,
                structlog.stdlib.add_logger_name,
                structlog.stdlib.add_log_level,
                structlog.stdlib.PositionalArgumentsFormatter(),
                structlog.processors.TimeStamper(fmt="iso"),
                structlog.processors.StackInfoRenderer(),
                structlog.processors.format_exc_info,
                structlog.processors.UnicodeDecoder(),
                structlog.dev.ConsoleRenderer(colors=False)
            ],
            context_class=dict,
            logger_factory=structlog.stdlib.LoggerFactory(),
            wrapper_class=structlog.stdlib.BoundLogger,
            cache_logger_on_first_use=True,
        )

        return structlog.get_logger("ServiceContainer")

    async def _initialize_redis(self):
        """Initialize async Redis connection"""
        import os

        redis_host = os.getenv('REDIS_HOST', 'localhost')
        redis_port = int(os.getenv('REDIS_PORT', 6379))
        redis_db = int(os.getenv('REDIS_DB', 0))
        redis_password = os.getenv('REDIS_PASSWORD', None)

        try:
            self.redis_client = aioredis.Redis(
                host=redis_host,
                port=redis_port,
                db=redis_db,
                password=redis_password,
                socket_timeout=5,
                socket_connect_timeout=5,
                retry_on_timeout=False,
                health_check_interval=30,
                max_connections=20,
                decode_responses=True
            )

            # Test connection
            await self.redis_client.ping()
            self.redis_available = True
            self.logger.info("redis_connected", host=redis_host, port=redis_port)

        except Exception as e:
            self.logger.warning("redis_connection_failed", error=str(e))
            self.redis_client = None
            self.redis_available = False

    async def _initialize_model_service(self):
        """Initialize model service asynchronously"""
        import os
        from ModelService import ModelService

        default_path = os.path.join(os.getcwd(), 'model_lstm_best.pth')
        model_path = os.getenv('MODEL_PATH', default_path)

        if not os.path.exists(model_path):
            self.logger.error("model_file_not_found", path=model_path)
            return

        try:
            # Load model in executor to avoid blocking
            loop = asyncio.get_event_loop()
            self.model_service = await loop.run_in_executor(
                None, ModelService, self.logger, model_path
            )
            self.logger.info("model_service_initialized", path=model_path)

        except Exception as e:
            self.logger.error("model_service_failed", error=str(e))
            self.model_service = None

    async def _initialize_active_learning(self):
        """Initialize active learning service (без production AL)"""
        if not self.redis_available or not self.model_service:
            self.logger.warning("active_learning_disabled",
                                redis_available=self.redis_available,
                                model_available=bool(self.model_service))
            return

        try:
            from ActiveLearningService import ActiveLearningService
            from RetrainService import RetrainService
            from CONFIG import Config

            config = Config()

            # Initialize retrain service
            retrain_logger = structlog.get_logger("RetrainService")
            self.retrain_service = RetrainService(
                self.redis_client, retrain_logger, self.model_service, config
            )

            # Initialize active learning service
            active_learning_logger = structlog.get_logger("ActiveLearningService")
            self.active_learning_service = ActiveLearningService(
                self.redis_client, active_learning_logger, self.retrain_service, config
            )

            # Link services
            self.active_learning_service.set_retrain_service(self.retrain_service)

            self.logger.info("active_learning_initialized_successfully",
                             services={
                                 "retrain_service": bool(self.retrain_service),
                                 "active_learning_service": bool(self.active_learning_service)
                             })

        except Exception as e:
            self.logger.error("active_learning_failed", error=str(e))
            self.active_learning_service = None
            self.retrain_service = None

    async def _initialize_basic_active_learning(self):
        """Fallback initialization without production AL"""
        try:
            from ActiveLearningService import ActiveLearningService
            from RetrainService import RetrainService
            from CONFIG import Config

            config = Config()

            retrain_logger = structlog.get_logger("RetrainService")
            self.retrain_service = RetrainService(
                self.redis_client, retrain_logger, self.model_service, config
            )

            active_learning_logger = structlog.get_logger("ActiveLearningService")
            self.active_learning_service = ActiveLearningService(
                self.redis_client, active_learning_logger, self.retrain_service, config
            )

            self.active_learning_service.set_retrain_service(self.retrain_service)
            self.production_al_service = None  # Mark as unavailable

            self.logger.info("basic_active_learning_initialized_fallback")

        except Exception as e:
            self.logger.error("basic_active_learning_failed", error=str(e))
            self.active_learning_service = None
            self.retrain_service = None
            self.production_al_service = None

class CacheService:
    """Optimized caching service"""

    def __init__(self, redis_client, logger, container: ServiceContainer):
        self.redis = redis_client
        self.logger = logger
        self.container = container

    @lru_cache(maxsize=1000)
    def _get_cache_key(self, problem_description: str) -> str:
        """Generate cache key"""
        import hashlib
        return f"prediction:{hashlib.md5(problem_description.encode()).hexdigest()}"

    async def get_cached_prediction(self, problem_description: str) -> Optional[PredictionResponse]:
        """Get cached prediction with proper validation"""
        if not self.redis:
            return None

        cache_key = self._get_cache_key(problem_description)

        try:
            cached_data = await self.redis.get(cache_key)
            if not cached_data:
                self.container.stats['cache_misses'] += 1
                return None

            # Parse and validate cached data
            cached_dict = json.loads(cached_data)

            # Handle datetime conversion
            if 'timestamp' in cached_dict and isinstance(cached_dict['timestamp'], str):
                cached_dict['timestamp'] = datetime.fromisoformat(cached_dict['timestamp'])

            # Validate with Pydantic
            response = PredictionResponse(**cached_dict)
            response.cached = True

            self.container.stats['cache_hits'] += 1
            self.logger.info("cache_hit", cache_key=cache_key)
            return response

        except (json.JSONDecodeError, ValidationError, ValueError) as e:
            # Clear corrupted cache
            await self.redis.delete(cache_key)
            self.logger.warning("cache_corruption_cleared", cache_key=cache_key, error=str(e))
            self.container.stats['cache_misses'] += 1
            return None
        except Exception as e:
            self.logger.warning("cache_get_failed", error=str(e))
            self.container.stats['cache_misses'] += 1
            return None

    async def set_cached_prediction(self, problem_description: str,
                                    response: PredictionResponse, expiry: int = 3600):
        """Cache prediction with proper serialization"""
        if not self.redis:
            return False

        cache_key = self._get_cache_key(problem_description)

        try:
            # Convert to dict for JSON serialization
            response_dict = response.dict()

            # Handle datetime serialization
            if 'timestamp' in response_dict:
                response_dict['timestamp'] = response_dict['timestamp'].isoformat()

            cached_data = json.dumps(response_dict, default=str)
            await self.redis.setex(cache_key, expiry, cached_data)

            self.logger.info("cache_set", cache_key=cache_key, expiry=expiry)
            return True

        except Exception as e:
            self.logger.warning("cache_set_failed", error=str(e))
            return False


def sanitize_logs(data) -> Any:
    """Sanitize sensitive data from logs"""
    if data is None:
        return None

    # Handle list (like batch requests)
    if isinstance(data, list):
        return [sanitize_logs(item) for item in data]

    # Handle non-dict types
    if not isinstance(data, dict):
        return data

    sensitive_fields = {'user_id', 'api_key', 'token', 'password', 'email'}

    def sanitize_value(key: str, value: Any) -> Any:
        if key.lower() in sensitive_fields:
            return '***'
        elif isinstance(value, dict):
            return {k: sanitize_value(k, v) for k, v in value.items()}
        elif isinstance(value, list):
            return [sanitize_value('', v) if isinstance(v, dict) else v for v in value]
        return value

    return {k: sanitize_value(k, v) for k, v in data.items()}


# Global container instance
container = ServiceContainer()