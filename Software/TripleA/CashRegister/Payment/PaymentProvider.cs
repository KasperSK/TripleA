using CashRegister.Models;

namespace CashRegister.Payment
{
    public abstract class PaymentProvider : IPaymentProvider
    {
        protected int Amount { get; set; }
        public int StartChange { get; protected set; }
        public abstract PaymentType Type { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }


        public abstract void Init();

        /// <summary>
        ///     Transfor the amount and describtion, and returns true og false whether the transaktion was a succes or not
        /// </summary>
        public abstract bool TransferAmount(int amount, string description = null);

        /// <summary>
        ///     Writes the transaktionstatus
        /// </summary>
        public abstract bool TransactionStatus();

        /// <summary>
        ///     Balance the payment
        /// </summary>
        public virtual int Tally()
        {
            return Amount;
        }

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