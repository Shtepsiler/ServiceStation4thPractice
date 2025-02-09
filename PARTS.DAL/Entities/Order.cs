using PARTS.DAL.Entities.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PARTS.DAL.Entities
{
    public class Order: Base
    {
        public Guid? СustomerId { get; set; }
        public bool IsPaid { get; set; } = false;
        public Status Status { get; set; } = Status.Pending;
        public int? OrderIndex { get; set; }
        public string WEIPrice { get; set; }
        public decimal TotalPrice => CalculateTotalPrice();
        public string? TransactionHash { get; set; } 

        public List<Part> Parts { get; set; } = new List<Part>();
        private decimal CalculateTotalPrice()
        {
            decimal total = 0;
            foreach (var part in Parts)
            {
                if(part.PriceRegular != null)
                total += part.PriceRegular.Value;
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
