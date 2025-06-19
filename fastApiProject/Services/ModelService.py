import numpy as np
import pandas as pd

from ModelInterraction.TextProcesing import preprocess_text
from ModelInterraction.DataLoader import load_data, load_stopwords,get_labels , load_dataset,save_data
from ModelInterraction.Model import GetModel, predict_mechanic, train_model_in_thread
from ModelInterraction.Statistics import get_model_statistics_json
from config import config

class ActiveLearningModelService:
    def __init__(self, logger, model_path=config.lstm_model_path, stopwords_path=config.stopwords_path):
        self.model_path = model_path
        self.model_checkpoint_path = model_path
        self.data = load_data(config.mechanic_data_path)
        self.stopwords = load_stopwords(stopwords_path)
        self.X_train, self.X_test, self.y_train, self.y_test, vocab_size, max_length, num_classes, label_encoder, tokenizer  = load_dataset(self.data, self.stopwords)
        self.tokenizer = tokenizer
        self.label_encoder = label_encoder
        self.model = GetModel(vocab_size=vocab_size, num_classes=num_classes, max_length=max_length,logger=logger)
        #train_model_in_thread(self.model, self.X_train, self.y_train, self.X_test, self.y_test)
        self.logger = logger
        self._cache_valid = False
    def getLabels(self):
        return get_labels(self.data)

    def predict(self, problem_description):
        predicted_class, confidence = predict_mechanic(problem_description, self.model, self.tokenizer,
                                                       max_length=self.model.input_shape[1],
                                                       label_encoder=self.label_encoder,
                                                       preprocess_text=preprocess_text,
                                                       custom_stop_words=self.stopwords)
        # Check if confidence is above the threshold
        if confidence > 0.7:
            # Add the problem and predicted label to the dataset
            new_entry = pd.DataFrame({
                'problem_description': [problem_description],
                'mechanic_class': [predicted_class]
            })

            # Append new entry to the in-memory data and save to CSV
            self.data = pd.concat([self.data, new_entry], ignore_index=True)
            save_data(self.data, config.mechanic_data_path)

            # Optionally, log this addition for tracking purposes
            self.logger.info(f"Added new high-confidence entry to dataset: {problem_description} -> {predicted_class} (Confidence: {confidence:.2f})")

        return predicted_class, confidence

    def _update_cache(self):
        """Precompute predictions when model changes"""
        loss, accuracy = self.model.evaluate(self.X_test, self.y_test, verbose=0)
        y_pred = self.model.predict(self.X_test, verbose=0)
        y_pred_class = np.argmax(y_pred, axis=1)

        self._cached_predictions = (y_pred, y_pred_class, loss, accuracy)
        self._cache_valid = True
    def get_statistics(self):
        if not self._cache_valid:
            self._update_cache()

        stats = get_model_statistics_json(
            self.model, self.X_test, self.y_test,
            self.label_encoder, self._cached_predictions
        )
        return stats

    def retrain(self, new_data, new_labels):
        # Load existing data and append new data
        new_df = pd.DataFrame({
            'problem_description': new_data,
            'mechanic_class': new_labels
        })

        # Validate labels against valid ones in the dataset
        invalid_labels = [label for label in new_labels if label not in self.getLabels()]
        if invalid_labels:
            raise ValueError(f"Invalid labels found: {invalid_labels}. Please use valid mechanic classes.")

        # Save the combined dataset to file
        updated_df = pd.concat([self.data, new_df], ignore_index=True)
        save_data(updated_df, config.mechanic_data_path)
        self.data = updated_df  # Update in-memory data

        # Reload the updated dataset for training
        self.X_train, self.X_test, self.y_train, self.y_test, vocab_size, max_length, num_classes, label_encoder, tokenizer = load_dataset(
            self.data, self.stopwords)

        # Update the model with new data in a separate thread
        self.model = GetModel(vocab_size=vocab_size, num_classes=num_classes, max_length=max_length, logger=self.logger)
        train_model_in_thread(self.model, self.X_train, self.y_train, self.X_test, self.y_test)

        self.logger.info("Model retraining with new data initiated.")