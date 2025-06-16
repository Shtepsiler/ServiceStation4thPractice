using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JOBS.BLL.DTOs.Requests;

public class BatchPredictionRequest
{
    [Required(ErrorMessage = "At least one problem must be provided")]
    [MinLength(1, ErrorMessage = "At least one problem must be provided")]
    [MaxLength(100, ErrorMessage = "Cannot process more than 100 problems at once")]
    [JsonPropertyName("problems")]
    public List<string> Problems { get; set; } = new();

    [StringLength(50)]
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }

    [JsonPropertyName("include_individual_ids")]
    public bool IncludeIndividualIds { get; set; } = true;
}