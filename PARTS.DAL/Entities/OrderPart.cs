using PARTS.DAL.Entities.Item;

namespace PARTS.DAL.Entities
{
    public class OrderPart
    {
        public Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }

        public Guid PartId { get; set; }
        public virtual Part? Part { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
