using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CashRegister.Models
{
    [ExcludeFromCodeCoverage]
    public class Product
    {
        private Product()
        {
        }

        public Product(string name, int price, bool saleable)
        {
            Name = name;
            Price = price;
            Saleable = saleable;
        }

        public long Id { get; private set; }

        public string Name { get; private set; }

        public int Price { get; set; }

        public bool Saleable { get; set; }

        public virtual ICollection<ProductGroup> ProductGroups { get; set; }
    }
}