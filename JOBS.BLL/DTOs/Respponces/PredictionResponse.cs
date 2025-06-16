using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JOBS.BLL.DTOs.Respponces;

public class PredictionResponse
{
    [Required]
    [MinLength(1, ErrorMessage = "At least one prediction must be provided")]
    [MaxLength(10, ErrorMessage = "Cannot return more than 10 predictions")]
    [JsonPropertyName("predictions")]
    public List<PredictionItem> Predictions { get; set; } = new();

    [Required]
    [JsonPropertyName("simple_predictions")]
    public List<Dictionary<string, double>> SimplePredictions { get; set; } = new();

    [Range(0.0, 1.0, ErrorMessage = "Confidence score must be between 0.0 and 1.0")]
    [JsonPropertyName("confidence_score")]
    public double ConfidenceScore { get; set; }

    [Required]
    [StringLength(32, MinimumLength = 8)]
    [JsonPropertyName("prediction_id")]
    public string PredictionId { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Processing time must be non-negative")]
    [JsonPropertyName("processing_time")]
    public double ProcessingTime { get; set; }

    [Required]
    [JsonPropertyName("ml_model_version")]
    public string MlModelVersion { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("cached")]
    public bool Cached { get; set; } = false;

    [JsonPropertyName("language_detected")]
    public string LanguageDetected { get; set; }

    // Helper properties
    [JsonIgnore]
    public bool IsHighConfidence => ConfidenceScore >= 0.8;

    [JsonIgnore]
    public PredictionItem TopPrediction => Predictions?.FirstOrDefault();
}