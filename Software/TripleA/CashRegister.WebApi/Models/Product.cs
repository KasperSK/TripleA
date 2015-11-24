using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CashRegister.WebApi.Models
{
    [ExcludeFromCodeCoverage]
    public class Product
    {
        public Product()
        {
            ProductGroups = new List<ProductGroup>();
            Id = 0;
        }

        public Product(string name, int price, bool saleable) : this()
        {
            Name = name;
            Price = price;
            Saleable = saleable;
        }

        public long Id { get; private set; }

        public string Name { get; private set; }

        public int Price { get; set; }

        public bool Saleable { get; set; }

        public virtual ICollection<ProductGroup> ProductGroups { get; }
    }

    public class ProductDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }

    public class ProductDetailsDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public bool Saleable { get; set; }

        public List<int> ProductGroups { get; set; }
    }
}