import torch
import torch.nn as nn


class LSTMAttentionModel(nn.Module):
    def __init__(self, vocab_size, num_parts, embed_dim=128, hidden_dim=256, enable_uncertainty=True):
        super().__init__()

        self.embedding = nn.Embedding(vocab_size, embed_dim, padding_idx=0)
        self.dropout_emb = nn.Dropout(0.1)

        self.lstm = nn.LSTM(
            embed_dim, hidden_dim // 2,
            num_layers=2,
            bidirectional=True,
            batch_first=True,
            dropout=0.2
        )

        self.attention = nn.Sequential(
            nn.Linear(hidden_dim, hidden_dim // 2),
            nn.Tanh(),
            nn.Linear(hidden_dim // 2, 1),
            nn.Softmax(dim=1)
        )

        self.classifier = nn.Sequential(
            nn.Linear(hidden_dim, hidden_dim),
            nn.LayerNorm(hidden_dim),
            nn.ReLU(),
            nn.Dropout(0.3),
            nn.Linear(hidden_dim, hidden_dim // 2),
            nn.LayerNorm(hidden_dim // 2),
            nn.ReLU(),
            nn.Dropout(0.2),
            nn.Linear(hidden_dim // 2, num_parts)
        )

        # OPTIONAL uncertainty components for backward compatibility
        self.enable_uncertainty = enable_uncertainty
        if enable_uncertainty:
            self.uncertainty_method = "monte_carlo"
            self.temperature = nn.Parameter(torch.ones(1))
            self.mc_dropout = nn.Dropout(0.5)
            self.uncertainty_attention = nn.Sequential(
                nn.Linear(hidden_dim, hidden_dim // 2),
                nn.Tanh(),
                nn.Linear(hidden_dim // 2, 1),
                nn.Softmax(dim=1)
            )
            self.uncertainty_head = nn.Linear(hidden_dim, 1)

    def forward(self, input_ids):
        embedded = self.embedding(input_ids)
        embedded = self.dropout_emb(embedded)

        lstm_out, _ = self.lstm(embedded)

        attention_weights = self.attention(lstm_out)
        attended = torch.sum(lstm_out * attention_weights, dim=1)

        logits = self.classifier(attended)
        return logits

    def load_state_dict(self, state_dict, strict=True):
        """Custom load_state_dict that handles missing uncertainty components"""
        # Check if this is an old model without uncertainty components
        uncertainty_keys = [
            'temperature', 'uncertainty_attention.0.weight', 'uncertainty_attention.0.bias',
            'uncertainty_attention.2.weight', 'uncertainty_attention.2.bias',
            'uncertainty_head.weight', 'uncertainty_head.bias'
        ]

        has_uncertainty = all(key in state_dict for key in uncertainty_keys)

        if not has_uncertainty and self.enable_uncertainty:
            print("Loading old model without uncertainty components - disabling uncertainty features")
            self.enable_uncertainty = False
            # Remove uncertainty components from the model
            if hasattr(self, 'temperature'):
                delattr(self, 'temperature')
            if hasattr(self, 'uncertainty_attention'):
                delattr(self, 'uncertainty_attention')
            if hasattr(self, 'uncertainty_head'):
                delattr(self, 'uncertainty_head')

        # Use non-strict loading to ignore missing uncertainty keys
        return super().load_state_dict(state_dict, strict=False)