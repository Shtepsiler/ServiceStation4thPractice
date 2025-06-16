using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JOBS.BLL.DTOs.Requests;

public class FeedbackRequest
{
    [Required(ErrorMessage = "Prediction ID is required")]
    [StringLength(32, MinimumLength = 8, ErrorMessage = "Prediction ID must be between 8 and 32 characters")]
    [JsonPropertyName("prediction_id")]
    public string PredictionId { get; set; }

    [Required(ErrorMessage = "At least one correct part must be specified")]
    [MinLength(1, ErrorMessage = "At least one correct part must be specified")]
    [MaxLength(20, ErrorMessage = "Cannot specify more than 20 correct parts")]
    [JsonPropertyName("correct_parts")]
    public List<string> CorrectParts { get; set; } = new();

    [Range(1, 5, ErrorMessage = "User rating must be between 1 and 5")]
    [JsonPropertyName("user_rating")]
    public int? UserRating { get; set; }

    [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters")]
    [JsonPropertyName("comments")]
    public string Comments { get; set; }

    [StringLength(50, ErrorMessage = "User ID cannot exceed 50 characters")]
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }

    [JsonPropertyName("is_correct_prediction")]
    public bool IsCorrectPrediction { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Time to feedback must be non-negative")]
    [JsonPropertyName("time_to_feedback")]
    public double? TimeToFeedback { get; set; }
}