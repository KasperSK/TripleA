using System.Web.UI.WebControls;

namespace CashRegister.WebApi.Models
{
    /// <summary>
    /// Model for OrderLine in Entity Framework
    /// </summary>
    public class OrderLine
    {
        /// <summary>
        /// Redundant?
        /// </summary>
        public OrderLine()
        {
            
        }

        /// <summary>
        /// The Id associatet with Orderlines is pk
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// How many items in the orderline
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// With product is being bougth
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        public int UnitPrice { get; set; }

        /// <summary>
        /// Is there a discount associatet with the orderline
        /// </summary>
        public virtual Discount Discount { get; set; }

        /// <summary>
        /// What the discount value is
        /// </summary>
        public int DiscountValue { get; set; }

        /// <summary>
        /// Witch salesorder does this line belong to?
        /// </summary>
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