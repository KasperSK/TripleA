using CashRegister.Models;

namespace CashRegister.Payment
{
    /// <summary>
    ///     The Cash drawers functionalities
    /// </summary>
    public class CashPayment : PaymentProvider
    {
        public CashPayment(int startChange)
        {
            StartChange = startChange;
        }

        public override PaymentType Type => PaymentType.Cash;
        public override string Name => "CashPayment";
        public override string Description => "CashPayment, nothing fancy here";

        public override void Shutdown()
        {
        }

        public override void Restart()
        {
        }

        public override void Init()
        {
        }

        public override bool TransferAmount(int amount, string description)
        {
            Amount += amount;
            return true;
        }

        public override bool TransactionStatus()
        {
            return true;
        }
    }
}