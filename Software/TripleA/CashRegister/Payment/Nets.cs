using System;
using System.Diagnostics.CodeAnalysis;
using CashRegister.Models;
using CashRegister.Payment;
using CashRegister.Log;

namespace CashRegister.Payment
{
    /// <summary>
    /// Payment provider implementation for nets
    /// </summary>
    public class Nets : PaymentProvider
    {
        /// <summary>
        /// To log events
        /// </summary>
        readonly ILogger _logger = LogFactory.GetLogger(typeof (Nets));

        /// <summary>
        /// Sets the payment type to nets
        /// </summary>
        public override PaymentType Type => PaymentType.Nets;

        /// <summary>
        /// Sets the name of the provider to nets
        /// </summary>
        public override string Name => "Nets";

        /// <summary>
        /// Sets the description of the provider
        /// </summary>
        public override string Description => "Nets Dankort/Visa";

        /// <summary>
        /// To initialize the provider
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override void Init()
        {
            _logger.Debug("Initializing");
        }

        /// <summary>
        /// To transfer an amount via nets
        /// </summary>
        /// <param name="amount">amount to transfer</param>
        /// <param name="description">Description of the transfer</param>
        /// <returns>Returns if the transaction went well</returns>
        public override bool TransferAmount(int amount, string description)
        {
            _logger.Debug("Transfering " + amount);
            Revenue += amount;
            return true;
        }

        /// <summary>
        /// To get the transaction status 
        /// </summary>
        /// <returns>True if went well</returns>
        [ExcludeFromCodeCoverage]
        public override bool TransactionStatus()
        {
            return true;
        }

        /// <summary>
        /// To retstart the transaction
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override void Restart()
        {
            _logger.Debug("Restarting");
            Shutdown();
            Init();
        }

        /// <summary>
        /// To shutdown the transaction
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override void Shutdown()
        {
            _logger.Debug("Shutting Down");
        }
	}
}
