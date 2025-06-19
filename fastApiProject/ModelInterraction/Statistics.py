from pydantic import BaseModel
from typing import List, Dict, Any
import numpy as np
from sklearn.metrics import classification_report, confusion_matrix


class Metrics(BaseModel):
    accuracy: float
    loss: float


class ClassificationReport(BaseModel):
    precision: float
    recall: float
    f1_score: float
    support: float


class ClassificationReportWrapper(BaseModel):
    accuracy: float
    macro_avg_precision: float
    macro_avg_recall: float
    macro_avg_f1: float
    weighted_avg_precision: float
    weighted_avg_recall: float
    weighted_avg_f1: float
    class_metrics: Dict[str, ClassificationReport]

    class Config:
        arbitrary_types_allowed = True


class ConfusionMatrix(BaseModel):
    class_name: str
    predictions: List[int]


class ModelStats(BaseModel):
    model_summary: str
    metrics: Metrics
    classification_report: ClassificationReportWrapper
    confusion_matrix: List[ConfusionMatrix]
    model_config = {'protected_namespaces': ()}  # Додай цю лінію


def get_model_statistics_json(model, X_test, y_test, label_encoder, cached_predictions=None):
    # Summary of the model
    model_summary = []
    model.summary(print_fn=lambda x: model_summary.append(x))

    # Use cached predictions if available
    if cached_predictions is not None:
        y_pred, y_pred_class, loss, accuracy = cached_predictions
    else:
        loss, accuracy = model.evaluate(X_test, y_test, verbose=0)
        y_pred = model.predict(X_test, verbose=0)
        y_pred_class = np.argmax(y_pred, axis=1)

    metrics = Metrics(accuracy=float(accuracy), loss=float(loss))

    # Detailed per-class statistics
    classification_rep = classification_report(
        y_test,
        y_pred_class,
        target_names=label_encoder.classes_,
        output_dict=True
    )

    # Convert all numbers to standard Python types
    classification_rep = {
        key: {
            sub_key: float(value) if isinstance(value, (np.float64, np.int64)) else value
            for sub_key, value in sub_dict.items()
        } if isinstance(sub_dict, dict) else sub_dict
        for key, sub_dict in classification_rep.items()
    }

    # Flatten classification report
    accuracy = classification_rep['accuracy']
    macro_avg_precision = classification_rep['macro avg']['precision']
    macro_avg_recall = classification_rep['macro avg']['recall']
    macro_avg_f1 = classification_rep['macro avg']['f1-score']
    weighted_avg_precision = classification_rep['weighted avg']['precision']
    weighted_avg_recall = classification_rep['weighted avg']['recall']
    weighted_avg_f1 = classification_rep['weighted avg']['f1-score']

    # Extract per-class metrics
    class_metrics = {
        key: ClassificationReport(
            precision=value['precision'],
            recall=value['recall'],
            f1_score=value['f1-score'],
            support=value['support']
        ) for key, value in classification_rep.items() if key not in ['accuracy', 'macro avg', 'weighted avg']
    }

    classification_report_obj = ClassificationReportWrapper(
        accuracy=accuracy,
        macro_avg_precision=macro_avg_precision,
        macro_avg_recall=macro_avg_recall,
        macro_avg_f1=macro_avg_f1,
        weighted_avg_precision=weighted_avg_precision,
        weighted_avg_recall=weighted_avg_recall,
        weighted_avg_f1=weighted_avg_f1,
        class_metrics=class_metrics
    )

    # Confusion Matrix in a format compatible with C#
    cm = confusion_matrix(y_test, y_pred_class)
    cm_list = [
        ConfusionMatrix(
            class_name=label_encoder.classes_[i],
            predictions=[int(x) for x in row]
        ) for i, row in enumerate(cm)
    ]

    # Create ModelStats object
    model_stats = ModelStats(
        model_summary="\n".join(model_summary),
        metrics=metrics,
        classification_report=classification_report_obj,
        confusion_matrix=cm_list
    )

    return model_stats