import os
import time
import gc
from typing import Dict, List, Tuple
import numpy as np
import torch
from functools import lru_cache
import torch.nn as nn

from LSTMAttentionModel import LSTMAttentionModel


class ModelService:
    def __init__(self, logger, model_path: str, device: str = "cpu"):
        self.logger = logger
        self.device = torch.device(device)
        self.model = None
        self.vocab = {}
        self.mlb = None
        self.model_version = "1.0.0"
        self.model_path = model_path
        self.enable_jit = False  # Відключи JIT для сумісності з retraining
        self._load_model()

    def _load_model(self):
        """Load the trained LSTM model with memory optimization and backward compatibility"""
        try:
            # Clear any existing model from memory
            if self.model is not None:
                del self.model
                torch.cuda.empty_cache() if torch.cuda.is_available() else None
                gc.collect()

            checkpoint = torch.load(self.model_path, map_location=self.device)

            self.vocab = checkpoint['vocab']
            self.mlb = checkpoint['mlb']
            vocab_size = checkpoint['vocab_size']
            num_parts = checkpoint['num_parts']

            # FIXED: Check if this is an old model without uncertainty components
            uncertainty_keys = [
                'temperature', 'uncertainty_attention.0.weight', 'uncertainty_attention.0.bias',
                'uncertainty_attention.2.weight', 'uncertainty_attention.2.bias',
                'uncertainty_head.weight', 'uncertainty_head.bias'
            ]

            state_dict = checkpoint['model_state_dict']
            has_uncertainty = all(key in state_dict for key in uncertainty_keys)

            # Create model with appropriate uncertainty setting
            self.model = LSTMAttentionModel(
                vocab_size,
                num_parts,
                enable_uncertainty=has_uncertainty
            ).to(self.device)

            # Load state dict (will handle missing uncertainty components automatically)
            self.model.load_state_dict(state_dict, strict=False)
            self.model.eval()

            # ВІДКЛЮЧЕНО JIT optimization для сумісності з retraining
            if self.enable_jit:
                try:
                    scripted_model = torch.jit.script(self.model)
                    if hasattr(torch.jit, 'optimize_for_inference'):
                        self.model = torch.jit.optimize_for_inference(scripted_model)
                        self.logger.info("model_optimized_for_inference")
                    else:
                        self.model = scripted_model
                        self.logger.info("model_scripted_for_inference")
                except Exception as e:
                    self.logger.warning("model_optimization_skipped", error=str(e))
            else:
                self.logger.info("jit_optimization_disabled_for_retraining")

            self.logger.info("model_loaded",
                             vocab_size=vocab_size,
                             num_parts=num_parts,
                             device=str(self.device),
                             jit_enabled=self.enable_jit,
                             uncertainty_enabled=has_uncertainty)

            # Clear checkpoint to free memory
            del checkpoint
            gc.collect()

        except Exception as e:
            self.logger.error("model_load_failed", error=str(e))
            raise

    @lru_cache(maxsize=10000)
    def _clean_text_cached(self, text: str) -> str:
        """Cached version of text cleaning"""
        return self._clean_text(text)

    def _clean_text(self, text: str) -> str:
        """Clean and preprocess input text"""
        import re

        if not text:
            return ""

        text = str(text).lower()

        # Ukrainian automotive terminology expansion
        replacements = {
            'троїть': 'нестабільний_двигун пропуски_запалювання',
            'скриплять': 'скрип_звук гальмівний_скрип',
            'не працює': 'поломка несправність відмова',
            'фари': 'світло освітлення лампи оптика',
            'дальнього': 'дальний_світло',
            'гуде': 'шум звук гудіння',
            'розганяється': 'потужність розгін тяга',
            'запускається': 'запуск стартер',
        }

        for old, new in replacements.items():
            if old in text:
                text = text.replace(old, f' {new} ')

        # Clean special characters, keep Ukrainian letters
        text = re.sub(r'[^\w\sа-яіїєґ_]', ' ', text)
        text = re.sub(r'\s+', ' ', text)

        return text.strip()

    def predict(self, problem_description: str, top_k: int = 5,
                threshold: float = 0.1) -> Tuple[List[Dict], float, float]:
        """Make prediction with memory optimization"""
        if not self.model:
            raise ValueError("Model not loaded")

        start_time = time.time()

        try:
            with torch.no_grad():
                # Preprocess text with caching
                clean_text = self._clean_text_cached(problem_description)
                words = clean_text.split()

                # Tokenize with bigrams
                tokens = words[:64]
                if len(words) > 1:
                    bigrams = [f"{words[i]}_{words[i + 1]}" for i in range(len(words) - 1)]
                    tokens.extend(bigrams[:64])

                # Convert to indices
                indices = [self.vocab.get(token, 1) for token in tokens[:128]]

                # Pad sequence
                if len(indices) < 128:
                    indices.extend([0] * (128 - len(indices)))

                # Create tensor and predict
                input_tensor = torch.LongTensor([indices]).to(self.device)

                # Forward pass
                logits = self.model(input_tensor)
                probabilities = torch.sigmoid(logits[0]).cpu().numpy()

                # Explicit cleanup
                del logits, input_tensor

                # Clear GPU cache if available
                if torch.cuda.is_available():
                    torch.cuda.empty_cache()

                # Get top predictions - ФІКС для top_k
                top_indices = np.argsort(probabilities)[::-1][:top_k]

                predictions = []
                for idx in top_indices:
                    confidence = float(probabilities[idx])
                    part_name = self.mlb.classes_[idx]

                    # Додай всі top_k результати, фільтруй по threshold тільки при високому threshold
                    if threshold <= 0.1 or confidence > threshold:
                        predictions.append({
                            'part': part_name,
                            'confidence': confidence
                        })

                # Гарантуй мінімум результатів
                if len(predictions) < min(3, top_k):
                    for idx in top_indices:
                        part_name = self.mlb.classes_[idx]
                        confidence = float(probabilities[idx])

                        if not any(p['part'] == part_name for p in predictions):
                            predictions.append({
                                'part': part_name,
                                'confidence': confidence
                            })

                        if len(predictions) >= top_k:
                            break

                # Calculate overall confidence
                overall_confidence = (
                    np.mean([p['confidence'] for p in predictions[:min(top_k, len(predictions))]])
                    if predictions else 0.0
                )

                processing_time = time.time() - start_time

                self.logger.info("prediction_completed",
                                 confidence=overall_confidence,
                                 processing_time=processing_time,
                                 predictions_count=len(predictions))

                return predictions, overall_confidence, processing_time

        except Exception as e:
            self.logger.error("prediction_failed", error=str(e))
            raise
        finally:
            # Cleanup
            gc.collect()

    async def batch_predict(self, problem_descriptions: List[str],
                            top_k: int = 5, threshold: float = 0.1) -> List[Tuple[List[Dict], float, float]]:
        """Batch prediction for efficiency"""
        if not self.model:
            raise ValueError("Model not loaded")

        results = []

        # Process in batches to avoid memory issues
        batch_size = 16
        for i in range(0, len(problem_descriptions), batch_size):
            batch = problem_descriptions[i:i + batch_size]

            batch_results = []
            for desc in batch:
                try:
                    result = self.predict(desc, top_k, threshold)
                    batch_results.append(result)
                except Exception as e:
                    self.logger.error("batch_prediction_item_failed",
                                      index=i, error=str(e))
                    # Return empty result for failed predictions
                    batch_results.append(([], 0.0, 0.0))

            results.extend(batch_results)

            # Cleanup between batches
            gc.collect()
            if torch.cuda.is_available():
                torch.cuda.empty_cache()

        return results

    def _load_model_from_path(self, model_path: str):
        """Load model from specific path with improved error handling"""
        try:
            # Validate file exists and is readable
            if not os.path.exists(model_path):
                raise FileNotFoundError(f"Model file not found: {model_path}")

            # Clear existing model state
            if self.model is not None:
                del self.model
                self.model = None
                torch.cuda.empty_cache() if torch.cuda.is_available() else None
                gc.collect()

            # Load and validate checkpoint
            checkpoint = torch.load(model_path, map_location=self.device)

            required_keys = ['model_state_dict', 'vocab', 'mlb', 'vocab_size', 'num_parts']
            missing_keys = [k for k in required_keys if k not in checkpoint]
            if missing_keys:
                raise ValueError(f"Invalid checkpoint structure - missing: {missing_keys}")

            # Update internal state FIRST
            self.vocab = checkpoint['vocab']
            self.mlb = checkpoint['mlb']
            vocab_size = checkpoint['vocab_size']
            num_parts = checkpoint['num_parts']

            # Validate state consistency
            if len(self.vocab) != vocab_size:
                self.logger.warning("vocab_size_inconsistency",
                                    file_vocab_size=vocab_size,
                                    actual_vocab_size=len(self.vocab))
                vocab_size = len(self.vocab)  # Use actual size

            if len(self.mlb.classes_) != num_parts:
                self.logger.warning("mlb_classes_inconsistency",
                                    file_num_parts=num_parts,
                                    actual_num_parts=len(self.mlb.classes_))
                num_parts = len(self.mlb.classes_)  # Use actual size

            # Create model with validated dimensions
            self.model = LSTMAttentionModel(
                vocab_size=vocab_size,
                num_parts=num_parts,
                enable_uncertainty=False  # Disable for compatibility
            ).to(self.device)

            # Load state dict with error handling
            state_dict = checkpoint['model_state_dict']

            # Filter out problematic keys
            filtered_state_dict = {}
            for name, param in state_dict.items():
                if not any(skip in name for skip in ['uncertainty', 'temperature']):
                    filtered_state_dict[name] = param

            # Load with size validation
            try:
                self.model.load_state_dict(filtered_state_dict, strict=False)
            except RuntimeError as e:
                if "size mismatch" in str(e):
                    self.logger.error("model_architecture_mismatch",
                                      error=str(e),
                                      suggestion="Model architecture changed")
                    raise ValueError(f"Model architecture mismatch: {e}")
                raise

            # Set to eval mode and validate
            self.model.eval()

            # Test model works
            try:
                test_input = torch.zeros((1, 128), dtype=torch.long).to(self.device)
                with torch.no_grad():
                    test_output = self.model(test_input)
                    if torch.isnan(test_output).any():
                        raise ValueError("Model produces NaN outputs")
            except Exception as e:
                raise ValueError(f"Model validation failed: {e}")

            # Update internal state
            self.model_path = model_path

            # Clear caches
            self.clear_cache()

            self.logger.info("model_reloaded_successfully",
                             path=model_path,
                             vocab_size=vocab_size,
                             num_parts=num_parts,
                             device=str(self.device))

        except Exception as e:
            self.logger.error("model_reload_failed",
                              path=model_path,
                              error=str(e))
            # Don't leave model in broken state
            self.model = None
            raise

    def _create_model_with_proper_architecture(self, vocab_size: int, num_parts: int, device) -> LSTMAttentionModel:
        """Create model with consistent architecture for retraining"""
        model = LSTMAttentionModel(
            vocab_size=vocab_size,
            num_parts=num_parts,
            enable_uncertainty=False  # Always disable for retraining
        ).to(device)

        # Apply proper weight initialization
        def init_weights(m):
            if isinstance(m, nn.Linear):
                torch.nn.init.xavier_uniform_(m.weight)
                if m.bias is not None:
                    torch.nn.init.zeros_(m.bias)
            elif isinstance(m, nn.LSTM):
                for name, param in m.named_parameters():
                    if 'weight' in name:
                        torch.nn.init.xavier_uniform_(param)
                    elif 'bias' in name:
                        torch.nn.init.zeros_(param)
            elif isinstance(m, nn.Embedding):
                torch.nn.init.normal_(m.weight, mean=0, std=0.1)

        model.apply(init_weights)
        return model
    def get_model_info(self) -> Dict:
        """Get model information"""
        return {
            "model_version": self.model_version,
            "vocab_size": len(self.vocab) if self.vocab else 0,
            "num_classes": len(self.mlb.classes_) if self.mlb else 0,
            "device": str(self.device),
            "model_loaded": self.model is not None,
            "model_path": self.model_path,
            "accuracy": getattr(self, 'accuracy', 0.9515),
            "jit_enabled": self.enable_jit
        }

    def clear_cache(self):
        """Clear text cleaning cache"""
        self._clean_text_cached.cache_clear()
        gc.collect()
        self.logger.info("model_cache_cleared")

    def enable_jit_optimization(self, enable: bool = True):
        """Toggle JIT optimization (disable for retraining compatibility)"""
        if self.enable_jit != enable:
            self.enable_jit = enable
            self._load_model()  # Reload model with new JIT setting
            self.logger.info("jit_optimization_toggled", enabled=enable)