using PARTS.BLL.DTOs.Responses;
using PARTS.DAL.Entities;

namespace PARTS.BLL.DTOs.Requests
{
    public class OrderRequest : BaseDTO
    {
        public Guid? СustomerId { get; set; }
        public int? OrderIndex { get; set; }
        public decimal TotalPrice { get; set; }
        public string? WEIPrice { get; set; }

        public Status Status { get; set; }
        public IEnumerable<PartResponse>? Parts { get; set; }

    }
}
