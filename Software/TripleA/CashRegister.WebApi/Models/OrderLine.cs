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

    /// <summary>
    /// Data transfer object for orderlines
    /// </summary>
    public class OrderlineDto
    {
        /// <summary>
        /// Id of the order line
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// What was the quantity sold for the orderline
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Name of the product on the orderline
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Price of a single unit on the orderline
        /// </summary>
        public int UnitPrice { get; set; }

        /// <summary>
        /// Discount applied to the orderline
        /// </summary>
        public int Discount { get; set; }
    }
}