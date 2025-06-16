using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JOBS.BLL.DTOs.Respponces;
public class PredictionItem
{
    [Required(ErrorMessage = "Part name is required")]
    [JsonPropertyName("part_name")]
    public string PartName { get; set; }

    [Range(0.0, 1.0, ErrorMessage = "Confidence must be between 0.0 and 1.0")]
    [JsonPropertyName("confidence")]
    public double Confidence { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("explanation")]
    public string Explanation { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Estimated cost must be non-negative")]
    [JsonPropertyName("estimated_cost")]
    public double? EstimatedCost { get; set; }

    // Helper properties (not serialized)
    [JsonIgnore]
    public string ConfidencePercentage => $"{Confidence:P1}";

    [JsonIgnore]
    public bool IsHighConfidence => Confidence >= 0.8;

    [JsonIgnore]
    public string FormattedCost => EstimatedCost?.ToString("C") ?? "N/A";

    public override string ToString() => $"{PartName} ({ConfidencePercentage})";
}
