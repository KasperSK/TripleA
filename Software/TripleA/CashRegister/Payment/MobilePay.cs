using System;
using System.Diagnostics.CodeAnalysis;
using CashRegister.Log;
using CashRegister.Models;

namespace CashRegister.Payment
{
    public class MobilePay : PaymentProvider
	{
        readonly ILogger _logger = LogFactory.GetLogger(typeof(MobilePay));

        public override PaymentType Type => PaymentType.MobilePay;
        public override string Name => "MobilePay";
        public override string Description => "MobilePay from Danske Bank";

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

