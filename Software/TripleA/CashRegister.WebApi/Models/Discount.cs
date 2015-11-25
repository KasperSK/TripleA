using System.Collections.Generic;

namespace CashRegister.WebApi.Models
{
    public class Discount
    {
       
        public Discount()
        {
            ProductGroups = new List<ProductGroup>();
        }

        public long Id { get; set; }

        public string Description { get; set; }

        public int Percent { get; set; }

        public virtual ICollection<ProductGroup> ProductGroups { get; }

    }
}
