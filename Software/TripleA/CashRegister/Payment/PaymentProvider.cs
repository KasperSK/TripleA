using CashRegister.Models;

namespace CashRegister.Payment
{
    public abstract class PaymentProvider : IPaymentProvider
    {
        protected PaymentProvider()
        {
            Revenue = 0;
        }

        /// <summary>
        ///     Balance the payment
        /// </summary>
        public int Revenue { get; protected set; }

        public int Tally()
        {
            var revenue = Revenue;
            Revenue = 0;
            return revenue;
        }


        public abstract PaymentType Type { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }

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