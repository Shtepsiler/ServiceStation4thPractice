using PARTS.DAL.Entities;

namespace PARTS.BLL.DTOs.Responses
{
    public class OrderResponse : BaseDTO
    {
        public Guid? СustomerId { get; set; }
        public int? OrderIndex { get; set; }
        public decimal TotalPrice { get; set; }
        public string? WEIPrice { get; set; }
        public bool IsPaid { get; set; }
        public Status Status { get; set; }
        public IEnumerable<PartResponse>? Parts { get; set; }
    }
}
