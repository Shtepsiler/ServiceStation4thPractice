namespace JOBS.BLL.DTOs.Requests
{
    public class AddPartsByCategoryRequest
    {
        public Guid OrderId { get; set; }
        public List<string> Categories { get; set; }
        public bool OnlyUniversal { get; set; } = true;

    }
}
