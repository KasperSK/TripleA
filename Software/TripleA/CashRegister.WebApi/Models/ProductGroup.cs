using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CashRegister.WebApi.Models
{
    [ExcludeFromCodeCoverage]
    public class ProductGroup
    {
        public ProductGroup()
        {
            Products = new List<Product>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; }
    }

    public class ProductGroupDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductGroupDetailsDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<long> Products { get; set; }
    }
}