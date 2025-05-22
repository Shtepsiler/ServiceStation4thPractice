using PARTS.DAL.Entities.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARTS.DAL.Entities
{
    public class OrderPart
    {
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        public Guid PartId { get; set; }
        public Part? Part { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
