namespace PARTS.DAL.Entities
{
    public class Order : Base
    {
        public Guid? JobId { get; set; }
        public Guid? СustomerId { get; set; }
        public bool IsPaid { get; set; } = false;
        public Status Status { get; set; } = Status.Pending;
        public int? OrderIndex { get; set; }
        public string WEIPrice { get; set; }
        public string? TransactionHash { get; set; }

        public virtual List<OrderPart> OrderParts { get; set; } = new();

        public decimal TotalPrice => CalculateTotalPrice();

        private decimal CalculateTotalPrice()
        {
            decimal total = 0;
            foreach (var orderPart in OrderParts)
            {
                if (orderPart.Part != null && orderPart.Part.PriceRegular != null)
                {
                    total += orderPart.Part.PriceRegular.Value * orderPart.Quantity;
                }
            }
            return total;
        }

        public override string ToString()
        {
            return $"Order ID: {Id}, Status: {Status}, Total: {TotalPrice} ETH, TxHash: {TransactionHash}";
        }
    }

    public enum Status
    {
        Pending,
        Paid
    }
}
