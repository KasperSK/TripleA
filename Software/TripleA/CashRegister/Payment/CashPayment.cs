using System.Diagnostics.CodeAnalysis;
using CashRegister.Log;
using CashRegister.Models;

namespace CashRegister.Payment
{
    /// <summary>
    ///     The Cash drawers functionalities
    /// </summary>
    public class CashPayment : PaymentProvider
    {
        readonly ILogger _logger = LogFactory.GetLogger(typeof(CashPayment));

        public override PaymentType Type => PaymentType.Cash;
        public override string Name => "CashPayment";
        public override string Description => "CashPayment, nothing fancy here";

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

        public override bool TransactionStatus()
        {
            return true;
        }

        public override void Restart()
        {
            _logger.Debug("Restarting");
            Shutdown();
            Init();
        }

        public override void Shutdown()
        {
            _logger.Debug("Shutting Down");
        }
    }
}