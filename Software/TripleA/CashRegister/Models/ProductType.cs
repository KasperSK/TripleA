using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CashRegister.Models
{
    [ExcludeFromCodeCoverage]
    public class ProductType
    {
        public ProductType()
        {
            ProductGroups = new List<ProductGroup>();
        }

        /// <summary>
        ///     Uniqe Id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     Name of the Type
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     Price of the Type
        /// </summary>
        public virtual int Price { get; set; }

        /// <summary>
        ///     Color associated with the group
        /// </summary>
        public virtual string Color { get; set; }

        /// <summary>
        ///     Groups of products to match this
        /// </summary>
        public virtual ICollection<ProductGroup> ProductGroups { get; }
    }
}