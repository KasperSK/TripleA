namespace CashRegister.Models
{
    using System.Collections.Generic;

    public sealed class ProductGroup
    {
        public ProductGroup()
        {

        }

        public long Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
