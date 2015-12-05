using System.Web.UI.WebControls;

namespace CashRegister.WebApi.Models
{
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

    public class OrderlineDto
    {
        public long Id { get; set; }

        public int Quantity { get; set; }

        public string ProductName { get; set; }

        public int UnitPrice { get; set; }

        public int Discount { get; set; }
    }
}