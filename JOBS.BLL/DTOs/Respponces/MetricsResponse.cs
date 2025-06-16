using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JOBS.BLL.DTOs.Respponces;

public class MetricsResponse
{
    [Range(0, int.MaxValue)]
    [JsonPropertyName("predictions_total")]
    public int PredictionsTotal { get; set; }

    [Range(0, int.MaxValue)]
    [JsonPropertyName("feedback_total")]
    public int FeedbackTotal { get; set; }

    [Range(0.0, 1.0)]
    [JsonPropertyName("accuracy_current")]
    public double AccuracyCurrent { get; set; }

    [Range(0.0, double.MaxValue)]
    [JsonPropertyName("feedback_ratio")]
    public double FeedbackRatio { get; set; }

    [Range(0.0, 1.0)]
    [JsonPropertyName("average_confidence")]
    public double AverageConfidence { get; set; }

    [Range(0.0, double.MaxValue)]
    [JsonPropertyName("average_processing_time")]
    public double AverageProcessingTime { get; set; }

    [Range(0.0, 1.0)]
    [JsonPropertyName("cache_hit_rate")]
    public double CacheHitRate { get; set; }

    [JsonPropertyName("active_learning_enabled")]
    public bool ActiveLearningEnabled { get; set; }

    [JsonPropertyName("ml_model_performance")]
    public Dictionary<string, double> MlModelPerformance { get; set; } = new();

    [JsonPropertyName("system_resources")]
    public Dictionary<string, double> SystemResources { get; set; } = new();

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.Now;
}
