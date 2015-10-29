namespace CashRegister.Models
{
    using System.Collections.Generic;

    public sealed class Product
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

        public ICollection<ProductGroup> ProductGroups { get; set; }
    }
}
