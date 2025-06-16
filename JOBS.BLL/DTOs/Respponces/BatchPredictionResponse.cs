using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JOBS.BLL.DTOs.Respponces;

public class BatchPredictionResponse
{
    [Required]
    [JsonPropertyName("results")]
    public List<PredictionResponse> Results { get; set; } = new();

    [Required]
    [JsonPropertyName("batch_id")]
    public string BatchId { get; set; }

    [Range(0, int.MaxValue)]
    [JsonPropertyName("total_processed")]
    public int TotalProcessed { get; set; }

    [Range(0.0, double.MaxValue)]
    [JsonPropertyName("total_processing_time")]
    public double TotalProcessingTime { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.Now;

    // Helper properties
    [JsonIgnore]
    public double AverageProcessingTime => TotalProcessed > 0 ? TotalProcessingTime / TotalProcessed : 0.0;

    [JsonIgnore]
    public bool AllSuccessful => Results?.All(r => !string.IsNullOrEmpty(r.PredictionId)) ?? false;
}