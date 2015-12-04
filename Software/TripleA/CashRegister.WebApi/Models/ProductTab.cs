using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CashRegister.WebApi.Models
{
    /// <summary>
    ///     Class to determine which products goes on which tab
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProductTab
    {
        /// <summary>
        /// Initializes list
        /// </summary>
        public ProductTab()
        {
            ProductTypes = new List<ProductType>();
        }
        /// <summary>
        ///     Uniqe Id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     Name of the Tab
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     In what order is this displayed
        /// </summary>
        public virtual int Priority { get; set; }

        /// <summary>
        ///     Is it currently active
        /// </summary>
        public virtual bool Active { get; set; }

        /// <summary>
        ///     Color of the tab
        /// </summary>
        public virtual string Color { get; set; }

        /// <summary>
        /// List of product types associatet with the tab
        /// </summary>
        public virtual ICollection<ProductType> ProductTypes { get; }
    }

    /// <summary>
    /// Data transfer object to send to the consumer of the webApi
    /// </summary>
    public class ProductTabDto
    {
        /// <summary>
        /// Id of the product tab
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the product tab
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Priority of the product tab
        /// </summary>
        public int Priority { get; set; }
    }

    /// <summary>
    /// Detailed data transfer object to send to the consumer of the webApi
    /// </summary>
    public class ProductTabDetailsDto
    {
        /// <summary>
        /// Id of the product tab
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the product tab
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Priority of the product tab
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// The active states of the product tab
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Color of the product tab in gui
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// List of product types ids 
        /// </summary>
        public List<int> ProductTypes { get; set; } 
    }
}