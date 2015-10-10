using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CashRegister.Log;
using CashRegister.Database;

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
            hat test = new hat();
        }

    }
}
