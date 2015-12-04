using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CashRegister.WebApi.Models
{
    /// <summary>
    /// Model for ProductGroups in Entity Framework
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProductGroup
    {
        /// <summary>
        /// Initializes the list of products
        /// </summary>
        public ProductGroup()
        {
            Products = new List<Product>();
        }

        /// <summary>
        /// Id of the product group
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name of the product group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of products associatet with this product group
        /// </summary>
        public virtual ICollection<Product> Products { get; }
    }

    /// <summary>
    /// Data transfer object to send to the consumer of the webApi
    /// </summary>
    public class ProductGroupDto
    {
        /// <summary>
        /// id of the productgroup reprecented by this dto
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name of the productgroup reprecented by this dto
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Detailed data transfer object to send to the consumer of the webApi
    /// </summary>
    public class ProductGroupDetailsDto
    {
        /// <summary>
        /// id of the productgroup reprecented by this dto
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name of the productgroup reprecented by this dto
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of product Id's associatet with this product group
        /// </summary>
        public List<long> Products { get; set; }
    }
}