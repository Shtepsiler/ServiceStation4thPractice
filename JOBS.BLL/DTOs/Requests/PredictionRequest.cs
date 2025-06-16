using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JOBS.BLL.DTOs.Requests;


public class PredictionRequest
{
    [Required(ErrorMessage = "Problem description is required")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Problem description must be between 10 and 1000 characters")]
    [JsonPropertyName("problem_description")]
    public string ProblemDescription { get; set; }

    [StringLength(50, ErrorMessage = "User ID cannot exceed 50 characters")]
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }

    [RegularExpression("^(uk|en|auto)$", ErrorMessage = "Language must be 'uk', 'en', or 'auto'")]
    [JsonPropertyName("language")]
    public string Language { get; set; } = "uk";

    [JsonPropertyName("include_explanations")]
    public bool IncludeExplanations { get; set; } = false;

    [Range(1, 10, ErrorMessage = "TopK must be between 1 and 10")]
    [JsonPropertyName("top_k")]
    public int TopK { get; set; } = 5;

    [Range(0.0, 1.0, ErrorMessage = "Confidence threshold must be between 0.0 and 1.0")]
    [JsonPropertyName("confidence_threshold")]
    public double ConfidenceThreshold { get; set; } = 0.1;
}