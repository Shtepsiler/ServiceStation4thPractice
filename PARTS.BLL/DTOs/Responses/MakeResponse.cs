using Newtonsoft.Json;

namespace PARTS.BLL.DTOs.Responses
{
    public class MakeResponse : BaseDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Year { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public List<VehicleResponse>? Vehicles { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public List<ModelResponse>? Models { get; set; }
    }
}
