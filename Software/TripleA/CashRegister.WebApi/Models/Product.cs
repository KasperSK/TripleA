using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CashRegister.WebApi.Models
{
    /// <summary>
    /// Model for Product in Entity Framework
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Product
    {
        /// <summary>
        /// Intializes the list of product groups and sets id to 0
        /// </summary>
        public Product()
        {
            ProductGroups = new List<ProductGroup>();
            Id = 0;
        }

        /// <summary>
        /// Initializes a product with specific parameters
        /// </summary>
        /// <param name="name">Name of the product</param>
        /// <param name="price">Price of the product</param>
        /// <param name="saleable">Is the product saleable</param>
        public Product(string name, int price, bool saleable) : this()
        {
            Name = name;
            Price = price;
            Saleable = saleable;
        }

        /// <summary>
        /// Id of the product
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Pricce of the product
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Is the product saleable
        /// </summary>
        public bool Saleable { get; set; }

        /// <summary>
        /// List of groups associatet with the product
        /// </summary>
        public virtual ICollection<ProductGroup> ProductGroups { get; }
    }

    /// <summary>
    /// Data transfer object to send to the consumer of the webApi
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Id of the product reprecentet by this dto
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name of the product reprecentet by this dto
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Detailed data transfer object to send to the consumer of the webApi
    /// </summary>
    public class ProductDetailsDto
    {
        /// <summary>
        /// Id of the product reprecentet by this dto
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name of the product reprecentet by this dto
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// price of the product reprecentet by this dto
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Is the product saleable as reprecentet by this dto
        /// </summary>
        public bool Saleable { get; set; }

        /// <summary>
        /// list of product group ID's reprecentet by this dto
        /// </summary>
        public List<long> ProductGroups { get; set; }
    }
}