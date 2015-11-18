namespace CashRegister.Models
{
    using System;
    using System.Collections.Generic;

    public class SalesOrder
    {
        public SalesOrder()
        {
            Transactions = new List<Transaction>();
            Lines = new List<OrderLine>();
        }

        public long Id { get; set; }

        public DateTime Date { get; set; }

        public int Total { get; set; }

        public virtual OrderStatus Status  { get; set; }

        public virtual ICollection<Transaction> Transactions { get;  }

        public virtual ICollection<OrderLine> Lines { get;  }
    }
}
