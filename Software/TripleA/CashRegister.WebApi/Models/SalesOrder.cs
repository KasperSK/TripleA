using System;
using System.Collections.Generic;
using System.Linq;

namespace CashRegister.WebApi.Models
{
    /// <summary>
    /// Enumeration of the orderstatus... Created/Completed/Failed
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Indicates a order has been created
        /// </summary>
        Created = 0,
        /// <summary>
        /// indicates a order has been completet
        /// </summary>
        Completed = 1,
        /// <summary>
        /// Indicates a order has failed
        /// </summary>
        Failed = 2
    }

    /// <summary>
    /// Model for SalesOrders in Entity Framework
    /// </summary>
    public class SalesOrder
    {
        /// <summary>
        /// Initializes lists
        /// </summary>
        public SalesOrder()
        {
            Transactions = new List<Transaction>();
            Lines = new List<OrderLine>();
        }

        /// <summary>
        /// Id of the sales order
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Date and time of the order
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The total sum of the sale
        /// </summary>
        public int Total
        {
            get { return Lines.Sum(p => p.UnitPrice * p.Quantity - p.DiscountValue); }
        }

        /// <summary>
        /// The status of the order
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// List of transactions for the order
        /// </summary>
        public virtual ICollection<Transaction> Transactions { get; }

        /// <summary>
        /// List of orderlines for the order
        /// </summary>
        public virtual ICollection<OrderLine> Lines { get; }
    }

    /// <summary>
    /// Simple data transfer object for salesorder
    /// </summary>
    public class SalesOrderDto
    {
        /// <summary>
        /// Id of the salesorder
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Date and time of the salesorder
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The total amounth paid
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Status of the order
        /// </summary>
        public OrderStatus Status { get; set; }
    }

    /// <summary>
    /// Detailed data transfer obect for the sales order
    /// </summary>
    public class SalesOrderDetailsDto
    {
        /// <summary>
        /// Id of the salesorder
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Date and time of the salesorder
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The total amounth paid
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Status of the order
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// List of transaction ids coupled to the order
        /// </summary>
        public List<long> Transactions { get; set; }

        /// <summary>
        /// List of orderlines ids coupled to the order
        /// </summary>
        public List<long> Lines { get; set; }
    }
}