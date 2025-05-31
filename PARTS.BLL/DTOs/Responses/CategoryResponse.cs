using Newtonsoft.Json;

namespace PARTS.BLL.DTOs.Responses
{
    public class CategoryResponse : BaseDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public CategoryImageResponse? CategoryImage { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<PartResponse>? Parts { get; set; }

    }
}
