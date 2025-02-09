using Microsoft.Identity.Client;
using System.Text.Json.Serialization;

namespace PARTS.DAL.Entities.Item
{
    public class Category : Base
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid? CategoryImageId { get; set; }
        [JsonIgnore] public CategoryImage? CategoryImage { get; set; }
        [JsonIgnore] public List<Part>? Parts { get; set; }

    }
}
