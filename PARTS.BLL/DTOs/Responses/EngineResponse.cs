using Newtonsoft.Json;

namespace PARTS.BLL.DTOs.Responses
{
    public class EngineResponse : BaseDTO
    {
        public int Cylinders { get; set; }
        public int Liter { get; set; }
        public DateTime? Year { get; set; }
        public string? Model { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public SubModelResponse? SubModel { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public MakeResponse? Make { get; set; }

    }
}
