using PARTS.DAL.Entities.Item;
using PARTS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARTS.BLL.DTOs.Responses
{
    public class OrderPartResponse
    {
        public Guid OrderId { get; set; }
        public OrderResponse Order { get; set; }

        public Guid PartId { get; set; }
        public PartResponse Part { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
