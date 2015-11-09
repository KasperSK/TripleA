namespace CashRegister.Models
{
    using System.Collections.Generic;

    public class ProductGroup
    {
        public ProductGroup()
        {

        }

        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
