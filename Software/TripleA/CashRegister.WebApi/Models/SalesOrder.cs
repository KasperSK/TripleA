using System;
using System.Collections.Generic;
using System.Linq;

namespace CashRegister.WebApi.Models
{
    public enum OrderStatus
    {
        Created = 0,
        Completed = 1,
        Failed = 2
    }

    public class SalesOrder
    {
        public SalesOrder()
        {
            Transactions = new List<Transaction>();
            Lines = new List<OrderLine>();
        }

        public long Id { get; set; }

        public DateTime Date { get; set; }

        public int Total
        {
            get { return Lines.Sum(p => p.UnitPrice * p.Quantity - p.DiscountValue); }
        }

        public OrderStatus Status { get; set; }

        public virtual ICollection<Transaction> Transactions { get; }

        public virtual ICollection<OrderLine> Lines { get; }
    }

    public class SalesOrderDto
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public OrderStatus Status { get; set; }
    }

    public class SalesOrderDetailsDto
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public int Total { get; set; }

        public OrderStatus Status { get; set; }

        public List<long> Transactions { get; set; }

        public List<long> Lines { get; set; }
    }
}