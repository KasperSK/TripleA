using System;
using CashRegister.Database;
using CashRegister.DAL;
using CashRegister.Log;
using CashRegister.Models;

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

            var cc = new CashRegisterContext();

            // Kalle Seed Data
            // var cc = new CashRegisterContext(new CashProductInitializer());
            
            cc.Database.Initialize(true);

            IDalFacade dalFacade = new DalFacade();

            using (var uow = dalFacade.GetUnitOfWork())
            {
                uow.ProductRepository.Insert(new Product("Hat", 100, true));
                uow.Save();
            }

            _logger = LogFactory.GetLogger(typeof (Program));
            _logger.Fatal("Fatal");
            _logger.Err("Error");
            _logger.Warn("Warn");
            _logger.Info("Info");
            _logger.Debug("Debug");

        }

    }
}
