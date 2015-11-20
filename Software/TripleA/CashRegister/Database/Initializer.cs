using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.ConstrainedExecution;
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
            base.Seed(context);
        }
    }

    public class InitHelper
    {
        private readonly CashRegisterContext _context;
        private readonly List<ProductGroup> _groups;
        private readonly List<Product> _products;
        private readonly List<ProductTab> _tabs;
        private readonly List<ProductType> _types;
        private ProductGroup _group;
        private Product _product;

        private ProductTab _tab;
        private ProductType _type;

        public InitHelper(CashRegisterContext context)
        {
            _context = context;
            _groups = new List<ProductGroup>();
            _tabs = new List<ProductTab>();
            _products = new List<Product>();
            _types = new List<ProductType>();
        }

        public void AddTab(string name, int priority, string color)
        {
            AddTab(name, priority, color, true);
        }

        public void AddTab(string name, int priority, string color, bool active)
        {
            _tab = new ProductTab
            {
                Active = active,
                Color = color,
                Name = name,
                Priority = priority,
            };
            _tabs.Add(_tab);
        }

        public void AddType(string name, int price, string color)
        {
            _type = new ProductType
            {
                Color = color,
                Name = name,
                Price = price,
            };
            _tab.ProductTypes.Add(_type);
            _types.Add(_type);
        }

        public void AddGroup(string name)
        {
            _group = new ProductGroup
            {
                Name = name,
            };
            _type.ProductGroups.Add(_group);
            _groups.Add(_group);
        }

        public void AddProduct(string name)
        {
            AddProduct(name, int.MaxValue, true);
        }

        public void AddProduct(string name, bool saleable)
        {
            AddProduct(name, int.MaxValue, saleable);
        }

        public void AddProduct(string name, int price)
        {
            AddProduct(name, price, true);
        }

        public void AddProduct(string name, int price, bool saleable)
        {
            if (price == int.MaxValue)
                price = _type.Price;
            _product = new Product(name, price, saleable);
            _group.Products.Add(_product);
            _products.Add(_product);
        }

        public void Save()
        {
            foreach (var product in _products)
            {
                _context.Products.Add(product);
            }

            foreach (var productGroup in _groups)
            {
                _context.ProductGroups.Add(productGroup);
            }

            foreach (var productType in _types)
            {
                _context.ProductTypes.Add(productType);
            }

            foreach (var productTab in _tabs)
            {
                _context.ProductTabs.Add(productTab);
            }
        }
    }

    public class FullProductInitializer : EmptyInitializer
    {
        protected override void Seed(CashRegisterContext context)
        {
            var s = new InitHelper(context);

            s.AddTab("Øl Fane", 0, "LimeGreen");

            s.AddType("Billig Øl Type", 12, "YellowGreen");
            s.AddGroup("Billig Øl Gruppe");
            s.AddProduct("Ceres Top");
            s.AddProduct("Royal Classic");

            s.AddType("Special Øl Type", 15, "GreenYellow");
            s.AddGroup("Special Øl Gruppe");
            s.AddProduct("Royal Export");
            s.AddProduct("Blå Thor");
            s.AddProduct("Heineken");
            s.AddProduct("Stout");
            s.AddProduct("Giraf Kalle");
            s.AddProduct("Havskum");

            s.AddType("Udenlandsk Øl Type", 15, "LawnGreen");
            s.AddGroup("Udenlandsk Øl Gruppe");
            s.AddProduct("Sol");
            s.AddProduct("Edelweiss");
            s.AddProduct("Newcastle");
            s.AddProduct("Moretti");
            s.AddProduct("Krusovice");

            s.AddType("Fadøl Type", 18, "Yellow");
            s.AddGroup("Fadøl Gruppe");
            s.AddProduct("Royal Fad");
            s.AddProduct("Jule Fad");

            s.AddTab("Drinks", 1, "DodgerBlue");

            s.AddType("Billig Drinks Type", 20, "LightSkyBlue");
            s.AddGroup("Billig Drinks Gruppe");
            s.AddProduct("Brandbil");
            s.AddProduct("Tequila Sunrise");
            s.AddProduct("Champangne Brus");
            s.AddProduct("Piña Colada");
            s.AddProduct("Southern Delight");
            s.AddProduct("Pawadise");
            s.AddProduct("Fidel Castro");
            s.AddProduct("Sommer Morgan");
            s.AddProduct("Pink Pussy");

            s.AddType("30 kr drinks", 30, "SkyBlue");
            s.AddGroup("30 kr Drinks Gruppe");
            s.AddProduct("Moscow Mule");
            s.AddProduct("Fake Cherry");
            s.AddProduct("Sweet Bombay");
            s.AddProduct("Den Hvide Enke");
            s.AddProduct("Irish Coffee");
            s.AddProduct("Kaptain Eventyr");
            s.AddProduct("White Russian");
            s.AddProduct("Vodka Redbull");
            s.AddProduct("Labre Larver");

            s.AddType("35 kr drinks", 35, "DeepSkyBlue");
            s.AddGroup("35 kr Drinks Gruppe");
            s.AddProduct("Gøglermælk");

            s.AddType("40 kr drinks", 40, "");
            s.AddGroup("40 kr Drinks Gruppe");
            s.AddProduct("Blå Batman");
            s.AddProduct("Party Hamster");
            s.AddProduct("Cosmopolitan");

            s.AddType("45 kr drinks", 45, "PowderBlue");
            s.AddGroup("45 kr Drinks Gruppe");
            s.AddProduct("Long Island Iced Tea");
            s.AddProduct("Memory Leak");
            s.AddProduct("K-Special");

            s.AddType("50 kr drinks", 50, "SteelBlue");
            s.AddGroup("50 kr Drinks Gruppe");
            s.AddProduct("Kold Krig (2 drinks)");

            s.AddTab("Shots", 2, "IndianRed");

            s.AddType("Billig Shots Type", 10, "DarkOrange");
            s.AddGroup("Billig Shots Gruppe");
            s.AddProduct("Jägermeister");
            s.AddProduct("Sambuca");
            s.AddProduct("Små Diverse");
            s.AddProduct("Tequila");
            s.AddProduct("Vodka");
            s.AddProduct("Rom");

            s.AddType("Alm Shots Type", 15, "OrangeRed");
            s.AddGroup("Alm Shots Gruppe");
            s.AddProduct("Arnbitter");
            s.AddProduct("Cointreau");
            s.AddProduct("Fernet Branca/Menta");
            s.AddProduct("Galliano");
            s.AddProduct("Gin");
            s.AddProduct("Khalua");
            s.AddProduct("Pisang Ambon");
            s.AddProduct("Southern Comfort");
            s.AddProduct("Whiskey");
            s.AddProduct("Bailey");
            s.AddProduct("Galliano Hotshots");

            s.AddType("Dyre Shots Type", 20, "Red");
            s.AddGroup("Dyre Shots Gruppe");
            s.AddProduct("Jägerbombs");
            s.AddProduct("Snefnugg");

            s.AddTab("SodaPopz", 3, "Coral");
            s.AddType("Sodapop", 25, "DarkSalmon");
            s.AddGroup("Sodapop");
            s.AddProduct("Blue Desire");
            s.AddProduct("Dirty Passion");
            s.AddProduct("Pure Exotic");
            s.AddProduct("White Lies");
            s.AddProduct("Senven Sins");
            s.AddProduct("Brezzer Lemon");


            s.Save();

            base.Seed(context);
        }
    }

    public class CashProductInitializer : EmptyInitializer
    {
        protected override void Seed(CashRegisterContext context)
        {
            var s = new InitHelper(context);

            s.AddTab("Øl fane", 0, "Green");
            s.AddType("Billig Øl", 12, "Yellow");
            s.AddGroup("Billig Øl Gruppe");
            s.AddProduct("Top");
            s.AddGroup("Billig Øl Gruppe 2");
            s.AddProduct("Classic");

            s.AddTab("Drinks", 1, "Blue");
            s.AddType("Billig Drinks", 20, "Orange");
            s.AddGroup("Billig Drinks");
            s.AddProduct("Tequilla Sunrise");
            s.AddProduct("Vodka Juice", 20, false);

            base.Seed(context);
        }
    }
}