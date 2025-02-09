using Newtonsoft.Json;

namespace PARTS.BLL.DTOs.Requests
{
    public class CategoryRequest : BaseDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public CategoryImageRequest? CategoryImage { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]

        public List<PartRequest>? Parts { get; set; }

    }
}
