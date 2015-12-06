using System;
using System.Diagnostics.CodeAnalysis;
using CashRegister.Log;
using CashRegister.Models;

namespace CashRegister.Payment
{

    /// <summary>
    /// Implementation of the mobilepay payment provider 
    /// </summary>
    public class MobilePay : PaymentProvider
	{
        /// <summary>
        /// To log events
        /// </summary>
        readonly ILogger _logger = LogFactory.GetLogger(typeof(MobilePay));

        /// <summary>
        /// Sets the payment type to mobilepay
        /// </summary>
        public override PaymentType Type => PaymentType.MobilePay;

        /// <summary>
        /// Set the name of the provider to mobilepay
        /// </summary>
        public override string Name => "MobilePay";

        /// <summary>
        /// sets the description for the providor
        /// </summary>
        public override string Description => "MobilePay from Danske Bank";

        /// <summary>
        /// To intialize the provider
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override void Init()
        {
            _logger.Debug("Initializing");
        }

        /// <summary>
        /// to transfer an amount of cash
        /// </summary>
        /// <param name="amount">The amount of cash to be transfered</param>
        /// <param name="description">The description of the transfer</param>
        /// <returns>Returns how the transaction went</returns>
        public override bool TransferAmount(int amount, string description)
        {
            _logger.Debug("Transfering " + amount);
            Revenue += amount;
            return true;
        }

        /// <summary>
        /// To get the status of the transaction
        /// </summary>
        /// <returns>True if went well else false</returns>
        [ExcludeFromCodeCoverage]
        public override bool TransactionStatus()
        {
            return true;
        }

        /// <summary>
        /// To restart the transaction
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override void Restart()
        {
            _logger.Debug("Restarting");
            Shutdown();
            Init();
        }

        /// <summary>
        /// to shutdown the transaction
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override void Shutdown()
        {
            _logger.Debug("Shutting Down");
        }
    }
}

