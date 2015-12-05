using System;

namespace CashRegister.WebApi.Models
{
    public enum TransactionStatus
    {
        Created = 0,
        Completed = 1,
        Failed = 2,
    }

    public enum PaymentType
    {
        Cash = 0,
        Nets = 1,
        Swipp = 2,
        MobilePay = 3,
    }

    public class Transaction
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int Price { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }

        public PaymentType PaymentType { get; set; }

        public TransactionStatus Status { get; set; }
    }

    public class TransactionDto
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int Price { get; set; }

        public PaymentType PaymentType { get; set; }

        public TransactionStatus Status { get; set; }
    }
}
