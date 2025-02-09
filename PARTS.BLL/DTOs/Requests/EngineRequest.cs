using Newtonsoft.Json;

namespace PARTS.BLL.DTOs.Requests
{
    public class EngineRequest : BaseDTO
    {
        public int Cylinders { get; set; }
        public int Liter { get; set; }
        public DateTime? Year { get; set; }
        public string? Model { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]

        [JsonIgnore]
        public SubModelRequest? SubModel { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public MakeRequest? Make { get; set; }

    }
}
