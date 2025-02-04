using PARTS.DAL.Entities.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARTS.DAL.Entities
{
    public class Order: Base
    {
        public Guid? СustomerId { get; set; }
        public List<Part> Parts { get; set; } = new List<Part>();
        public bool IsPaid { get; set; } = false;

        public string? TransactionHash { get; set; } 
        public Status Status { get; set; } = Status.Pending; 

        public decimal TotalPrice => CalculateTotalPrice();

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
