using System.Diagnostics.CodeAnalysis;
using CashRegister.Log;
using CashRegister.Models;

namespace CashRegister.Payment
{
    /// <summary>
    /// The Cash drawers functionalities
    /// </summary>
    public class CashPayment : PaymentProvider
    {
        /// <summary>
        /// To log events
        /// </summary>
        readonly ILogger _logger = LogFactory.GetLogger(typeof(CashPayment));

        /// <summary>
        /// Set the payment type to cash
        /// </summary>
        public override PaymentType Type => PaymentType.Cash;

        /// <summary>
        /// Sets the name to cash paymeny
        /// </summary>
        public override string Name => "CashPayment";

        /// <summary>
        /// Description of the payment
        /// </summary>
        public override string Description => "CashPayment, nothing fancy here";

        /// <summary>
        /// To initialize the payment 
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override void Init()
        {
            _logger.Debug("Initializing");
        }

        /// <summary>
        /// To transfer money for cash we just add to the total and assume the payment went well
        /// </summary>
        /// <param name="amount">Amount to transfer</param>
        /// <param name="description"></param>
        /// <returns>Weather we was succesfull in our transfer or not</returns>
        public override bool TransferAmount(int amount, string description)
        {
            _logger.Debug("Transfering " + amount);
            Revenue += amount;
            return true;
        }

        /// <summary>
        /// Return the status of the transaction
        /// </summary>
        /// <returns>Cash is allways true</returns>
        public override bool TransactionStatus()
        {
            return true;
        }

        /// <summary>
        /// To restart a transaction
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override void Restart()
        {
            _logger.Debug("Restarting");
            Shutdown();
            Init();
        }

        /// <summary>
        /// To stop a transaction
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override void Shutdown()
        {
            _logger.Debug("Shutting Down");
        }
    }
}