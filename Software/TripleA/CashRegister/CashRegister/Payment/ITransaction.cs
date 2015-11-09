using System.Collections.Generic;

namespace CashRegister.Payment
{
    public interface ITransaction
    {
        IPaymentProvidorDescriptor PaymentDescriptor { get; set; }
        int Amount { get; set; }

        string Description { get; set; }

        int ID { get; }

        IEnumerable<ITransactionObserver> Observers { get; set; }

        TransactionStatus Status { get; set; }

        void AddObserver(ITransactionObserver observer);
    }
}