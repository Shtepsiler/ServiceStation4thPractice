from fastapi import FastAPI, HTTPException, Body
from pydantic import BaseModel, validator
from typing import Optional, List
import logging
import os
import warnings
from Services.ModelService import ActiveLearningModelService
from ModelInterraction.Statistics import ModelStats
from fastapi.middleware.cors import CORSMiddleware
import asyncio
from concurrent.futures import ThreadPoolExecutor
os.environ['TF_ENABLE_ONEDNN_OPTS'] = '0'
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'
warnings.filterwarnings("ignore", category=UserWarning)
app = FastAPI(
    title="Active Learning Model Service",
    description="API для роботи з моделлю активного навчання.",
    version="1.0.0",
    docs_url="/swagger",  # URL для Swagger UI
    redoc_url="/redoc",  # URL для ReDoc (альтернативний формат документації)
    openapi_url="/openapi.json"  # URL для OpenAPI JSON
)
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  # Дозволити всі джерела
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)
# Configure logging
logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(levelname)s - %(message)s')
logger = logging.getLogger(__name__)

executor = ThreadPoolExecutor(max_workers=1)

# Instantiate the ActiveLearningModelService
model_service = ActiveLearningModelService(logger)
valid_labels = model_service.getLabels()

class ProblemDescription(BaseModel):
    description: str
    @validator('description')
    def validate_description(cls, v):
        if not v or len(v.strip()) < 10:
            raise ValueError("Description must be at least 10 characters")
        return v.strip()
class PredictionResponse(BaseModel):
    predicted_class: str
    confidence: float



class RetrainRequest(BaseModel):
    new_data: List[str]
    new_labels: List[str]

    @validator('new_labels')
    def validate_labels(cls, value):
        # Remove invalid labels that are not in the predefined valid labels
        filtered_labels = [label for label in value if label in valid_labels]

        # If no labels are valid, raise an error
        if len(filtered_labels) == 0:
            raise ValueError("None of the provided labels are valid.")

        return filtered_labels

class ActiveLearningRequest(BaseModel):
    X_initial: List[List[int]]  # List of sequences
    y_initial: List[str]  # Labels as strings
    X_pool: List[List[int]]
    y_pool: List[str]
    iterations: Optional[int] = 5
    uncertainty_threshold: Optional[float] = 0.5

    @validator('y_initial', 'y_pool')
    def validate_labels(cls, value):
        if not all(isinstance(label, str) and label.strip() != "" for label in value):
            raise ValueError("All labels must be non-empty strings.")
        return value

@app.post("/predict", response_model=PredictionResponse)
async def predict(problem: ProblemDescription):
    try:
        predicted_class, confidence = model_service.predict(problem.description)
        return PredictionResponse(predicted_class=predicted_class, confidence=confidence)
    except Exception as e:
        logger.error(f"Error during prediction: {e}")
        raise HTTPException(status_code=500, detail="Prediction failed")
@app.get("/")
async def root():
    return {"message": "Active Learning Model Service is running."}



@app.get("/model-statistics", response_model=ModelStats)
async def model_statistics():
    try:
        loop = asyncio.get_event_loop()
        stats = await loop.run_in_executor(executor, model_service.get_statistics)
        return stats
    except Exception as e:
        logger.error(f"Error fetching model statistics: {e}")
        raise HTTPException(status_code=500, detail="Failed to fetch model statistics")

@app.post("/retrain")
async def retrain_model(request: RetrainRequest):
    try:
        # Retrain the model with new data
        model_service.retrain(request.new_data, request.new_labels)
        return {"message": "Model retrained successfully with the new data."}
    except ValueError as e:
        logger.error(f"Validation Error during retraining: {e}")
        raise HTTPException(status_code=400, detail=str(e))
    except Exception as e:
        logger.error(f"Error during model retraining: {e}")
        raise HTTPException(status_code=500, detail="Retraining failed")


