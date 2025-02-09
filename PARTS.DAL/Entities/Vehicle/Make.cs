using System.Text.Json.Serialization;

namespace PARTS.DAL.Entities.Vehicle
{
    public class Make : Base
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Сountry { get; set; }
        public DateTime? Year { get; set; }

        [JsonIgnore] public List<Vehicle>? Vehicles { get; set; } = new List<Vehicle>();
        [JsonIgnore] public List<Model>? Models { get; set; } = new List<Model>();
        [JsonIgnore] public List<Engine>? Engines { get; set; } = new List<Engine>();
    }
}
