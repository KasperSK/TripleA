using System;
using System.Diagnostics.CodeAnalysis;
using CashRegister.Models;
using CashRegister.Payment;
using CashRegister.Log;

namespace CashRegister.Payment
{
    
    public class Nets : PaymentProvider
    {
        readonly ILogger _logger = LogFactory.GetLogger(typeof (Nets));

        public override PaymentType Type => PaymentType.Nets;
        public override string Name => "Nets";
        public override string Description => "Nets Dankort/Visa";

        [ExcludeFromCodeCoverage]
        public override void Init()
        {
            _logger.Debug("Initializing");
        }

        public override bool TransferAmount(int amount, string description)
        {
            _logger.Debug("Transfering " + amount);
            Revenue += amount;
            return true;
        }

        [ExcludeFromCodeCoverage]
        public override bool TransactionStatus()
        {
            return true;
        }

        [ExcludeFromCodeCoverage]
        public override void Restart()
        {
            _logger.Debug("Restarting");
            Shutdown();
            Init();
        }

        [ExcludeFromCodeCoverage]
        public override void Shutdown()
        {
            _logger.Debug("Shutting Down");
        }
	}
}
