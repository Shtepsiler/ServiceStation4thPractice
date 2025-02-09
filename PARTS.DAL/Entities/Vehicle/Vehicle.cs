using PARTS.DAL.Entities.Item;
using System.Text.Json.Serialization;

namespace PARTS.DAL.Entities.Vehicle
{
    public class Vehicle : Base
    {
        public string? FullModelName
        {
            get
            {
                var makeTitle = Make?.Title ?? string.Empty;
                var modelTitle = Model?.Title ?? string.Empty;
                var subModelTitle = SubModel?.Title ?? string.Empty;
                return $"{makeTitle} {modelTitle} {subModelTitle}".Trim();
            }
            set { }
        }

        public string? VIN { get; set; }
        public DateTime? Year { get; set; }
        public Guid? MakeId { get; set; }
        public Guid? ModelId { get; set; }
        public Guid? SubModelId { get; set; }
        public Guid? EngineId { get; set; }
        [JsonIgnore]
        public Make? Make { get; set; }
        [JsonIgnore]
        public Model? Model { get; set; }
        [JsonIgnore]
        public SubModel? SubModel { get; set; }
        [JsonIgnore]
        public Engine? Engine { get; set; }

        public string? URL { get; set; }
        [JsonIgnore]
        public List<Part>? Parts { get; set; } = new List<Part>();
    }
}
