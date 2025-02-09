using Newtonsoft.Json;

namespace PARTS.BLL.DTOs.Requests
{
    public class MakeRequest : BaseDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Year { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public List<VehicleRequest>? Vehicles { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]

        [JsonIgnore]
        public List<ModelRequest>? Models { get; set; }
    }
}
