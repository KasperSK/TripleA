using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using CashRegister.Log;
using CashRegister.Models;
using EfEnumToLookup.LookupGenerator;

namespace CashRegister.Database
{
    public class EmptyInitializer : DropCreateDatabaseAlways<CashRegisterContext>
    {
        protected override void Seed(CashRegisterContext context)
        {
            var enumToLookup = new EnumToLookup();
            enumToLookup.Apply(context);
        }
    }

    public class CashProductInitializer : DropCreateDatabaseAlways<CashRegisterContext>
    {
        protected override void Seed(CashRegisterContext context)
        {
            var enumToLookup = new EnumToLookup();
            enumToLookup.Apply(context);

            var export = new Product("Export", 15, true);
            context.Products.Add(export);

            var classic = new Product("Classic", 12, true);
            context.Products.Add(classic);

            var gol = new ProductGroup
            {
                Name = "Øl Gruppe",
                Products = new List<Product> {export, classic},
            };
            context.ProductGroups.Add(gol);
            context.ProductTabs.Add(new ProductTab
            {
                Active = true,
                Name = "Øl Fane",
                Priority = 2,
                Color = "Blue",
                ProductGroups = new List<ProductGroup> { gol }
            });




            var bb = new Product("Blå Batman", 40, true);
            context.Products.Add(bb);

            var ks = new Product("K Special", 48, true);
            context.Products.Add(ks);

            var gdrinks = new ProductGroup
            {
                Name = "Drinks Gruppe",
                Products = new List<Product> { bb, ks },
            };

            context.ProductGroups.Add(gdrinks);
            context.ProductTabs.Add(new ProductTab
            {
                Active = true,
                Name = "Drinks Fane",
                Priority = 3,
                Color = "Red",
                ProductGroups = new List<ProductGroup> { gdrinks }
            });



            var ss = new Product("Små Sure", 10, true);
            context.Products.Add(ss);

            var gj = new Product("Gajol", 10, false);
            context.Products.Add(gj);

            var gshots = new ProductGroup
            {
                Name = "Shots Gruppe",
                Products = new List<Product> { ss, gj },
            };

            context.ProductGroups.Add(gshots);
            context.ProductTabs.Add(new ProductTab
            {
                Active = true,
                Name = "Shots Fane",
                Priority = 5,
                Color = "Yellow",
                ProductGroups = new List<ProductGroup> { gshots }
            });



            var kc = new Product("Kims Chips", 20, false);
            context.Products.Add(kc);

            var gsnacks = new ProductGroup()
            {
                Name = "Snacks Gruppe",
                Products = new List<Product>() {kc},
            };
            context.ProductGroups.Add(gsnacks);
            context.ProductTabs.Add(new ProductTab
            {
                Active = true,
                Name = "Snacks Fane",
                Priority = 4,
                Color = "Green",
                ProductGroups = new List<ProductGroup> { gsnacks }
            });





            var gis = new ProductGroup
            {
                Name = "Is Gruppe",
                Products = new List<Product>(),
            };
            context.ProductGroups.Add(gis);
            context.ProductTabs.Add(new ProductTab
            {
                Active = true,
                Name = "Is Fane",
                Priority = 6,
                Color = "Gray",
                ProductGroups = new List<ProductGroup> {gis}
            });
            



            base.Seed(context);
        }
    }

    public class FullCashProductInitializer : DropCreateDatabaseAlways<CashRegisterContext>
    {
        ILogger _logger = new Logger(typeof(FullCashProductInitializer));

        protected override void Seed(CashRegisterContext context)
        {
            var oel = GetProductList("../../../CashRegister/CashRegister/Database/Oel.txt");

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
                Color = "Green",
                ProductGroups = new List<ProductGroup> { gol }
            });


            var drinks = GetProductList("../../../CashRegister/CashRegister/Database/drinks.txt");



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
                Color = "Yellow",
                ProductGroups = new List<ProductGroup> { gdrinks }
            });


            var shots = GetProductList("../../../CashRegister/CashRegister/Database/shots.txt");

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
                Color = "Blue",
                ProductGroups = new List<ProductGroup> { gshots }
            });



            var kc = new Product("Kims Chips", 20, true);
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
                Color = "Red",
                ProductGroups = new List<ProductGroup> { gsnacks }
            });


            var soda = GetProductList("../../../CashRegister/CashRegister/Database/soda.txt");


            var gSoda = new ProductGroup
            {
                Name = "Soda Popz Gruppe",
                Products = soda,
            };
            context.ProductGroups.Add(gSoda);
            context.ProductTabs.Add(new ProductTab
            {
                Active = true,
                Name = "Soda Popz Fane",
                Priority = 6,
                Color = "White",
                ProductGroups = new List<ProductGroup> { gSoda }
            });

            base.Seed(context);
        }

        private List<Product> GetProductList(string path)
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
                _logger.Debug(ex.Message);
            }

            return productList;
        }

    }

}
