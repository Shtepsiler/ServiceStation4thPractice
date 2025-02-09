using Newtonsoft.Json;

namespace PARTS.BLL.DTOs.Requests
{
    public class VehicleRequest : BaseDTO
    {
        public string? FullModelName { get; set; }
        public string? VIN { get; set; }
        public DateTime? Year { get; set; }
        public string? URL { get; set; }
        [JsonIgnore]
        public MakeRequest? Make { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public ModelRequest? Model { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public SubModelRequest? SubModel { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public EngineRequest? Engine { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public List<PartRequest>? Parts { get; set; }
    }
}
