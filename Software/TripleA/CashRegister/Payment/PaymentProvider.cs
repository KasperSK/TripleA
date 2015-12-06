using CashRegister.Models;

namespace CashRegister.Payment
{
    /// <summary>
    /// Base class for payment providers
    /// </summary>
    public abstract class PaymentProvider : IPaymentProvider
    {
        /// <summary>
        /// Constructor to set revenue to 0
        /// </summary>
        protected PaymentProvider()
        {
            Revenue = 0;
        }

        /// <summary>
        /// To get the revenue that this provider has accumulatet
        /// </summary>
        public int Revenue { get; protected set; }

        /// <summary>
        /// Return and resets the revenue
        /// </summary>
        /// <returns></returns>
        public int Tally()
        {
            var revenue = Revenue;
            Revenue = 0;
            return revenue;
        }

        /// <summary>
        /// TO hold the payment type
        /// </summary>
        public abstract PaymentType Type { get; }

        /// <summary>
        /// To hold the payment name
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// To hold the payment description
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// To init the payment provider
        /// </summary>
        public abstract void Init();

        /// <summary>
        ///     Transfor the amount and describtion, and returns true og false whether the transaktion was a succes or not
        /// </summary>
        public abstract bool TransferAmount(int amount, string description);

        /// <summary>
        ///     Writes the transaktionstatus
        /// </summary>
        public abstract bool TransactionStatus();

        /// <summary>
        ///     Shuts down the payment system
        /// </summary>
        public abstract void Shutdown();

        /// <summary>
        ///     Restart the payment system
        /// </summary>
        public abstract void Restart();
    }
}