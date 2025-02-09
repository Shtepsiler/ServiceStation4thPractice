using Newtonsoft.Json;

namespace PARTS.BLL.DTOs.Responses
{
    public class VehicleResponse : BaseDTO
    {
        public string? FullModelName { get; set; }
        public string? VIN { get; set; }
        public DateTime? Year { get; set; }
        public string? URL { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public MakeResponse? Make { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public ModelResponse? Model { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public SubModelResponse? SubModel { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public EngineResponse? Engine { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public List<PartResponse>? Parts { get; set; }
    }
}
