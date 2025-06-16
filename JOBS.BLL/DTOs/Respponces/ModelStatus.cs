using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JOBS.BLL.DTOs.Respponces;
public class ModelStatus
{
    [Required]
    [JsonPropertyName("ml_model_version")]
    public string MlModelVersion { get; set; }

    [Range(0.0, 1.0, ErrorMessage = "Accuracy must be between 0.0 and 1.0")]
    [JsonPropertyName("accuracy")]
    public double Accuracy { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Total predictions must be non-negative")]
    [JsonPropertyName("total_predictions")]
    public int TotalPredictions { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Feedback count must be non-negative")]
    [JsonPropertyName("feedback_count")]
    public int FeedbackCount { get; set; }

    [JsonPropertyName("last_retrain")]
    public DateTime? LastRetrain { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Active learning queue must be non-negative")]
    [JsonPropertyName("active_learning_queue")]
    public int ActiveLearningQueue { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Model file size must be non-negative")]
    [JsonPropertyName("model_file_size")]
    public int? ModelFileSize { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Training samples must be non-negative")]
    [JsonPropertyName("training_samples")]
    public int? TrainingSamples { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Classes count must be non-negative")]
    [JsonPropertyName("classes_count")]
    public int? ClassesCount { get; set; }

    // Helper properties
    [JsonIgnore]
    public string AccuracyPercentage => $"{Accuracy:P2}";

    [JsonIgnore]
    public double FeedbackRatio => TotalPredictions > 0 ? (double)FeedbackCount / TotalPredictions : 0.0;

    [JsonIgnore]
    public string FormattedModelSize => ModelFileSize.HasValue ?
        $"{ModelFileSize.Value / (1024.0 * 1024.0):F1} MB" : "Unknown";
}
