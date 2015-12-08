using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CashRegister.Models
{
    /// <summary>
    /// Statuses that en SalesOrder can have.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Order has been created and inserted to the database
        /// </summary>
        Created = 0,

        /// <summary>
        /// Order has been fully payed
        /// </summary>
        Completed = 1,

        /// <summary>
        /// Order did not get finished, saved for history
        /// </summary>
        Failed = 2,
    }

    [ExcludeFromCodeCoverage]
    public class SalesOrder
    {
        public SalesOrder()
        {
            Transactions = new List<Transaction>();
            Lines = new List<OrderLine>();
        }

        /// <summary>
        /// Unique Id for SalesOrder
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Last time this was changed
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Total amount to be paid
        /// </summary>
        public int Total
        {
            get { return Lines.Sum(p => p.UnitPrice * p.Quantity - p.DiscountValue); }
        }

        /// <summary>
        /// See OrderStatus enum
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// All Transactions that has been used to pay this SalesOrder
        /// </summary>
        public virtual ICollection<Transaction> Transactions { get; }

        /// <summary>
        /// All OrderLines added to the SalesOrder
        /// </summary>
        public virtual ICollection<OrderLine> Lines { get; }
    }
}