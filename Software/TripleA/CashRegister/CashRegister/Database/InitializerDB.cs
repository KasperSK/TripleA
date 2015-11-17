using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Net.Http.Headers;
using System.Windows.Annotations;
using System.Windows.Media;
using CashRegister.Models;

namespace CashRegister.Database
{
    public class FullCashProductInitializer : DropCreateDatabaseAlways<CashRegisterContext>
    {
        protected override void Seed(CashRegisterContext context)
        {
            var oel = getProductList("../../../CashRegister/CashRegister/Database/Oel.txt");

            var gol = new ProductGroup
            {
                Name = "Øl Gruppe",
                Products = oel
            };
            context.ProductGroups.Add(gol);
            context.ProductTabs.Add(new ProductTab
            {
                Active = true,
                Name = "Øl Fane",
                Priority = 2,
                ProductGroups = new List<ProductGroup> { gol }
            });


            var drinks = getProductList("../../../CashRegister/CashRegister/Database/drinks.txt");

            

            var gdrinks = new ProductGroup
            {
                Name = "Drinks Gruppe",
                Products = drinks
            };

            context.ProductGroups.Add(gdrinks);
            context.ProductTabs.Add(new ProductTab
            {
                Active = true,
                Name = "Drinks Fane",
                Priority = 3,
                ProductGroups = new List<ProductGroup> { gdrinks }
            });


            var shots = getProductList("../../../CashRegister/CashRegister/Database/shots.txt");

            var gshots = new ProductGroup
            {
                Name = "Shots Gruppe",
                Products = shots
            };

            context.ProductGroups.Add(gshots);
            context.ProductTabs.Add(new ProductTab
            {
                Active = true,
                Name = "Shots Fane",
                Priority = 5,
                ProductGroups = new List<ProductGroup> { gshots }
            });



            var kc = new Product("Kims Chips", 20, false);
            context.Products.Add(kc);

            var gsnacks = new ProductGroup()
            {
                Name = "Snacks Gruppe",
                Products = new List<Product>() { kc },
            };
            context.ProductGroups.Add(gsnacks);
            context.ProductTabs.Add(new ProductTab
            {
                Active = true,
                Name = "Snacks Fane",
                Priority = 4,
                ProductGroups = new List<ProductGroup> { gsnacks }
            });


            var soda = getProductList("../../../CashRegister/CashRegister/Database/soda.txt");


            var gSoda = new ProductGroup
            {
                Name = "Soda Popz Gruppe",
                Products = new List<Product>(),
            };
            context.ProductGroups.Add(gSoda);
            context.ProductTabs.Add(new ProductTab
            {
                Active = true,
                Name = "Soda Popz Fane",
                Priority = 6,
                ProductGroups = new List<ProductGroup> { gSoda }
            });




            base.Seed(context);
        }

        private List<Product> getProductList(string path)
        {
            var productList = new List<Product>();

            try
            {
                var fs = new FileStream(@path, FileMode.Open, FileAccess.Read);
                var reader = new StreamReader(fs);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var fields = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (!(fields.Length < 3))
                    {
                        var newProduct = new Product(fields[0], Convert.ToInt32(fields[1]), Convert.ToBoolean(fields[2]));
                        productList.Add(newProduct);
                    }
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return productList;
        }
    
    }
}
