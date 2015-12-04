using System.Collections.Generic;

namespace CashRegister.WebApi.Models
{
    /// <summary>
    /// Model for Discount in Entity Framework
    /// </summary>
    public class Discount
    {
       /// <summary>
       /// Constructor initializes list of productgroups
       /// </summary>
        public Discount()
        {
            ProductGroups = new List<ProductGroup>();
        }

        /// <summary>
        /// The Discounts Db Id is primary key
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Discription of what the discount is for
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Discount in percent %
        /// </summary>
        public int Percent { get; set; }

        /// <summary>
        /// The list of groups associatet with the discount
        /// </summary>
        public virtual ICollection<ProductGroup> ProductGroups { get; }

    }
}
