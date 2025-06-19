import yaml
import os
from pathlib import Path


class Config:
    def __init__(self, config_path="config.yaml"):
        self.config_path = config_path
        self._config = self._load_config()
        self._ensure_directories()

    def _load_config(self):
        if not os.path.exists(self.config_path):
            raise FileNotFoundError(f"Config file not found: {self.config_path}")

        with open(self.config_path, 'r', encoding='utf-8') as f:
            return yaml.safe_load(f)

    def _ensure_directories(self):
        """Create directories if they don't exist"""
        for category in self._config['paths'].values():
            if isinstance(category, dict):
                for path in category.values():
                    Path(path).parent.mkdir(parents=True, exist_ok=True)

    @property
    def lstm_model_path(self):
        return self._config['paths']['models']['lstm_model']

    @property
    def best_model_path(self):
        return self._config['paths']['models']['best_model']

    @property
    def checkpoint_path(self):
        return self._config['paths']['models']['checkpoint']

    @property
    def mechanic_data_path(self):
        return self._config['paths']['data']['mechanic_data']

    @property
    def stopwords_path(self):
        return self._config['paths']['data']['stopwords']

    @property
    def tokenizer_path(self):
        return self._config['paths']['helpers']['tokenizer']

    @property
    def label_encoder_path(self):
        return self._config['paths']['helpers']['label_encoder']

    # Model parameters
    @property
    def embedding_dim(self):
        return self._config['model']['embedding_dim']

    @property
    def lstm_units(self):
        return self._config['model']['lstm_units']

    @property
    def dense_units1(self):
        return self._config['model']['dense_units1']

    @property
    def dense_units2(self):
        return self._config['model']['dense_units2']

    @property
    def max_words(self):
        return self._config['model']['max_words']

    @property
    def confidence_threshold(self):
        return self._config['model']['confidence_threshold']

    # Training parameters
    @property
    def test_size(self):
        return self._config['training']['test_size']

    @property
    def random_state(self):
        return self._config['training']['random_state']

    @property
    def epochs(self):
        return self._config['training']['epochs']

    @property
    def batch_size(self):
        return self._config['training']['batch_size']

    @property
    def patience(self):
        return self._config['training']['patience']


# Global config instance
config = Config()