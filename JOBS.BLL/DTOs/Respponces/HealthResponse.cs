using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JOBS.BLL.DTOs.Respponces;

public class HealthResponse
{
    [Required]
    [RegularExpression("^(healthy|degraded|unhealthy)$", ErrorMessage = "Status must be 'healthy', 'degraded', or 'unhealthy'")]
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("ml_model_loaded")]
    public bool MlModelLoaded { get; set; }

    [JsonPropertyName("redis_connected")]
    public bool RedisConnected { get; set; }

    [Required]
    [JsonPropertyName("version")]
    public string Version { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Uptime must be non-negative")]
    [JsonPropertyName("uptime_seconds")]
    public double? UptimeSeconds { get; set; }

    [JsonPropertyName("environment")]
    public Dictionary<string, object> Environment { get; set; } = new();

    [JsonPropertyName("memory_usage")]
    public Dictionary<string, double> MemoryUsage { get; set; } = new();

    [JsonPropertyName("gpu_available")]
    public bool? GpuAvailable { get; set; }

    // Helper properties
    [JsonIgnore]
    public bool IsHealthy => Status == "healthy";

    [JsonIgnore]
    public TimeSpan? Uptime => UptimeSeconds.HasValue ? TimeSpan.FromSeconds(UptimeSeconds.Value) : null;
}