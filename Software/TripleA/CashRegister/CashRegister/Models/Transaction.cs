namespace CashRegister.Models
{
    using System;

    public class Transaction
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public int Price { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }

        public virtual PaymentType Paymenttype { get; set; }
    }
}
