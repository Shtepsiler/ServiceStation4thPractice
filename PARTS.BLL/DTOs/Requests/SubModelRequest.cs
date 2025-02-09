using Newtonsoft.Json;

namespace PARTS.BLL.DTOs.Requests
{
    public class SubModelRequest : BaseDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime Year { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public ModelRequest Model { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public List<VehicleRequest>? Vehicles { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public List<EngineRequest>? Engines { get; set; }

    }
}
