using System.Diagnostics.CodeAnalysis;

namespace CashRegister.Models
{
    [ExcludeFromCodeCoverage]
    public class OrderLine
    {
        public OrderLine()
        {
            
        }

        public long Id { get; set; }

        public int Quantity { get; set; }

        public virtual Product Product { get; set; }

        public int UnitPrice { get; set; }

        public virtual Discount Discount { get; set; }

        public int DiscountValue { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }


    }
}