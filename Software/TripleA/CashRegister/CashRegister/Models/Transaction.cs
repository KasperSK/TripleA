using CashRegister.Payment;

namespace CashRegister.Models
{
    using System;

    public enum TransactionStatus
    {
        Created = 0,
        Completed = 1,
        Failed = 2,
    }

    public class Transaction
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int Price { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }

        public virtual IPaymentProvidorDescriptor Paymenttype { get; set; }

        public TransactionStatus Status { get; set; }
    }
}
