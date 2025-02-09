using System.Text.Json.Serialization;

namespace PARTS.DAL.Entities.Vehicle
{
    public class SubModel : Base
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Transmission { get; set; }
        public int? Weight { get; set; }

        public DateTime? Year { get; set; }
        [JsonIgnore] public Model? Model { get; set; }
        [JsonIgnore] public List<Vehicle>? Vehicles { get; set; } = new List<Vehicle>();
        [JsonIgnore] public List<Engine>? Engines { get; set; } = new List<Engine>();

    }
}
