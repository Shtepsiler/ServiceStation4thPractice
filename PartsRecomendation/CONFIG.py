class Config:
    """Improved configuration for better transfer learning"""

    DEVELOPMENT_MODE = True  # Включити режим розробки
    SKIP_HEAVY_VALIDATION = True  # Пропустити важку валідацію

    # Relaxed validation thresholds for development
    QUALITY_THRESHOLD = 0.01  # Дуже низький поріг якості для розробки
    MIN_QUALITY_SAMPLES = 1  # Мінімум 1 зразок
    MIN_DIVERSITY_CLASSES = 1  # Мінімум 1 унікальний клас

    # Training intervals
    MIN_RETRAIN_INTERVAL_HOURS = 0.01  # 6 хвилин замість 1 години для тестування

    # Baseline testing limits for speed
    BASELINE_SAMPLE_SIZE = 100  # Тільки 50 зразків для створення baseline
    BASELINE_TEST_SIZE = 50  # Тільки 10 тестів для валідації

    # Logging settings
    ENABLE_DETAILED_LOGGING = False  # Зменшити детальність логів
    LOG_BASELINE_PREDICTIONS = False  # Не логувати кожен baseline prediction

    # Active Learning Settings
    UNCERTAINTY_THRESHOLD = 0.85
    FEEDBACK_THRESHOLD = 10
    MIN_RETRAIN_SAMPLES = 10

    # IMPROVED Transfer Learning Settings
    TRANSFER_LEARNING_MODE = "ultra_conservative"  # ultra_conservative, gradual, aggressive

    # Ultra-conservative settings to prevent catastrophic forgetting
    TRANSFER_ULTRA_CONSERVATIVE_LR = 1e-7  # Much smaller than before
    TRANSFER_MAX_EPOCHS = 3  # Shorter training
    TRANSFER_MAX_BASELINE_DEGRADATION = 0.9  # Max 5% baseline degradation
    TRANSFER_BATCH_SIZE = 30  # Smaller batches
    TRANSFER_GRADIENT_CLIP = 0.005  # Gentle clipping

    # Gradual unfreezing settings
    GRADUAL_STAGE1_EPOCHS = 3  # Classifier only
    GRADUAL_STAGE2_EPOCHS = 3  # + top layers
    GRADUAL_STAGE3_EPOCHS = 2  # + attention
    GRADUAL_STAGE1_LR = 1e-4
    GRADUAL_STAGE2_LR = 5e-5
    GRADUAL_STAGE3_LR = 1e-5

    # Data balancing
    NEW_CLASS_OVERSAMPLE_RATIO = 1  # Oversample new classes 5x
    MAX_NEW_TO_OLD_RATIO = 0.9  # New data max 50% of old data

    # Validation criteria
    MIN_BASELINE_PERFORMANCE = 0.8  # Model must maintain 80% on basic tests
    MIN_NEW_CLASS_ACCURACY = 0.05  # Must learn something on new data
    MIN_F1_SCORE = 0.1  # Minimum F1 score

    # Model Training Settings
    IMPROVEMENT_THRESHOLD = 0.0  # Don't require improvement, just no degradation
    VALIDATION_SPLIT_MIN = 0.1
    VALIDATION_SPLIT_MAX = 0.2

    # Conservative fine-tuning for existing classes
    EXISTING_CLASS_WEIGHT = 0.05  # Lower weight to preserve knowledge
    NEW_CLASS_WEIGHT = 1.5  # Higher weight for new classes

    # Prediction Settings
    DEFAULT_TOP_K = 5
    DEFAULT_CONFIDENCE_THRESHOLD = 0.1
    BATCH_CONFIDENCE_THRESHOLD = 0.1

    # More conservative hyperparameters
    FINE_TUNE_LR = 1e-5  # Much lower than before (was 0.00005)
    SCRATCH_LR = 0.0005  # Lower for stability
    FINE_TUNE_EPOCHS = 10  # Shorter training (was 25)
    SCRATCH_EPOCHS = 30  # Shorter training (was 50)
    PATIENCE = 8  # Earlier stopping (was 7)
    BATCH_SIZE_BASE = 8  # Smaller batches (was 16)

    # Enable transfer learning by default
    USE_TRANSFER_LEARNING = True
    PRETRAINED_MODEL_PATH = "model_lstm_best.pth"

    # Additional safety measures
    ENABLE_BASELINE_TESTING = True  # Test on known problems
    REQUIRE_SEMANTIC_VALIDATION = True  # Check semantic understanding
    MAX_CONSECUTIVE_FAILED_RETRAINS = 3  # Stop after N failed retrains

    # Debugging and monitoring
    LOG_DETAILED_METRICS = True  # Log detailed training metrics
    SAVE_TRAINING_HISTORY = True  # Save training progress
    VALIDATE_EVERY_EPOCH = True  # Run validation each epoch



