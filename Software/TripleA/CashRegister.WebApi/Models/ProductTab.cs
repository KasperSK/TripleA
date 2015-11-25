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


        public virtual ICollection<ProductType> ProductTypes { get; }
    }

    public class ProductTabDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
    }

    public class ProductTabDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public bool Active { get; set; }
        public string Color { get; set; }
        public List<int> ProductTypes { get; set; } 
    }
}