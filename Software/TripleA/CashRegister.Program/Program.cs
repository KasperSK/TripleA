using System;
using System.Data.Entity;
using CashRegister.Database;
using CashRegister.DAL;
using CashRegister.Log;
using CashRegister.Products;

namespace CashRegister.Program
{
    internal class Program
    {
        private static ILogger _logger;

        static Program()
        {
            LogFactory.Configure();
        }

        private static void Main(string[] args)
        {
            _logger = LogFactory.GetLogger(typeof (Program));

            IDatabaseInitializer<CashRegisterContext> seed;

            // Empty
            //seed = new EmptyInitializer();

            // Kalle Seed
            //seed = new CashProductInitializer();

            // Lærke Seed
            seed = new FullProductInitializer();

            using (var contex = new CashRegisterContext(seed))
            {
                contex.Database.Initialize(true);
            }


            IDalFacade dalFacade = new DalFacade();
            IProductDao pd = new ProductDao(dalFacade);
            IProductController pc = new ProductController(pd);


            Console.WriteLine("ProductTabs");
            foreach (var productTab in pc.ProductTabs)
            {
                Console.WriteLine(productTab.Priority + ": " + productTab.Name);
                foreach (var productType in productTab.ProductTypes)
                {
                    Console.WriteLine("\t" + productType.Name);
                    foreach (var productGroup in productType.ProductGroups)
                    {
                        Console.WriteLine("\t\t" + productGroup.Name);
                        foreach (var product in productGroup.Products)
                        {
                            Console.WriteLine("\t\t\t" + product.Name);
                        }
                    }
                }
            }


            _logger.Fatal("Fatal");
            _logger.Err("Error");
            _logger.Warn("Warn");
            _logger.Info("Info");
            _logger.Debug("Debug");
        }
    }
}