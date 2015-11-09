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

            defaultProducts.Add(new Product("Export", 15, true));
            defaultProducts.Add(new Product("Classic", 12, true));
            defaultProducts.Add(new Product("Blå Batman", 40, true));
            defaultProducts.Add(new Product("Små Sure", 10, true));

            foreach (var defaultProduct in defaultProducts)
            {
                context.Products.Add(defaultProduct);
            }

            base.Seed(context);
        }
    }

    
}
