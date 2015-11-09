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

            // Kalle Seed Data
            // var cc = new CashRegisterContext(new CashProductInitializer());
            
            cc.Database.Initialize(true);

            _logger = LogFactory.GetLogger(typeof (Program));
            _logger.Fatal("Fatal");
            _logger.Error("Error");
            _logger.Warn("Warn");
            _logger.Info("Info");
            _logger.Debug("Debug");

        }

    }
}
