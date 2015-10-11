using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CashRegister.CashRegister.Database;
using CashRegister.Log;
using CashRegister.Database;
using CashRegister.DAL;

namespace CashRegister.Program
{
    class Program
    {
        static Program()
        {
            LogFactory.Configure();
        }

        private static ILogger _logger;
        static void Main(string[] args)
        {
            _logger = LogFactory.GetLogger(typeof (Program));
            _logger.Fatal("Fatal");
            _logger.Error("Error");
            _logger.Warn("Warn");
            _logger.Info("Info");
            _logger.Debug("Debug");
            ProductUnitOfWork ProductUoW = new ProductUnitOfWork(new KasseApparat());
            var PG = new ProductGroup();
            var P = new Product();
            var PR = new Price();
            PG.GroupName = "UoWGroup";
            PG.Products.Add(P);
            PR.Product = P;
            PR.StartDate = new DateTime(1992,12,12);
            PR.EndDate = new DateTime(2000,12,13);
            P.ProductName = "UoWtest";
            P.ProductGroups.Add(PG);
            P.Prices.Add(PR);
            ProductUoW.ProductGroupRepository.Insert(PG);
            ProductUoW.PriceRepository.Insert(PR);
            ProductUoW.ProductRepository.Insert(P);
            ProductUoW.Save();
            Console.ReadKey();
        }

    }
}
