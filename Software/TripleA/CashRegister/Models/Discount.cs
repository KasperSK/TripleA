using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CashRegister.Models
{
    [ExcludeFromCodeCoverage]
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
