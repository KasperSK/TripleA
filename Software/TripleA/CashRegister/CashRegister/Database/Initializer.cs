using System.Collections.Generic;
using System.Data.Entity;
using CashRegister.Models;

namespace CashRegister.Database
{
    public class CashProductInitializer : DropCreateDatabaseAlways<CashRegisterContext>
    {
        protected override void Seed(CashRegisterContext context)
        {
            IList<Product> defaultProducts = new List<Product>();

            var export = new Product("Export", 15, true);
            defaultProducts.Add(export);

            var classic = new Product("Classic", 12, true);
            defaultProducts.Add(classic);

            var bb = new Product("Blå Batman", 40, true);
            defaultProducts.Add(bb);

            var ss = new Product("Små Sure", 10, true);
            defaultProducts.Add(ss);

            foreach (var defaultProduct in defaultProducts)
            {
                context.Products.Add(defaultProduct);
            }

            IList<ProductGroup> defaultGroups = new List<ProductGroup>();

            defaultGroups.Add(new ProductGroup
            {
                Name = "Øl",
                Products = new List<Product> { export, classic},
            });

            context.ProductGroups.Add(defaultGroups[0]);

            base.Seed(context);
        }
    }

    
}
