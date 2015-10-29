using System;
using CashRegister.Database;
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
            _logger = LogFactory.GetLogger(typeof (Program));
            _logger.Fatal("Fatal");
            _logger.Error("Error");
            _logger.Warn("Warn");
            _logger.Info("Info");
            _logger.Debug("Debug");
            var p = new Product("Flasek", 20, true);
            cc.Products.Add(p);
            cc.SaveChanges();
            _logger.Debug("Debug");
            Console.ReadKey();
        }

    }
}
