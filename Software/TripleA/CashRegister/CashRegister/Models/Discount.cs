namespace CashRegister.Models
{
    using System.Collections.Generic;

    public  class Discount
    {
       
        public Discount()
        {
            
        }

        public long Id { get; set; }

        public string Description { get; set; }

        public int Percent { get; set; }

        public virtual ICollection<ProductGroup> ProductGroups { get; set; }

    }
}
