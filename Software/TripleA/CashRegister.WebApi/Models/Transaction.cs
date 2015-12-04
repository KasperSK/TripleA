using System;

namespace CashRegister.WebApi.Models
{
    /// <summary>
    /// Enumeration of the transaction status weather it has been completed or it has failed
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// To indicate a transaction has been created
        /// </summary>
        Created = 0,
        /// <summary>
        /// To indicate a transaction has been completed
        /// </summary>
        Completed = 1,
        /// <summary>
        /// To indicate a transaction has failed
        /// </summary>
        Failed = 2,
    }

    /// <summary>
    /// Enumeration of the type of payment used be it cash, card or mobilephone
    /// </summary>
    public enum PaymentType
    {
        /// <summary>
        /// To indicate cash payment
        /// </summary>
        Cash = 0,
        /// <summary>
        /// TO indicate card payment
        /// </summary>
        Nets = 1,
        /// <summary>
        /// To indicate swipp payment
        /// </summary>
        Swipp = 2,
        /// <summary>
        /// To indicate mobilepay payment
        /// </summary>
        MobilePay = 3,
    }

    /// <summary>
    /// Model for Transaction in Entity Framework
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Id of the transaktion in the db is pk
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Description of the transaction 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Date and time when the transaction occured
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Price of the transaction
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Salesorder which the transaction was made on
        /// </summary>
        public virtual SalesOrder SalesOrder { get; set; }

        /// <summary>
        /// The type of payment
        /// </summary>
        public PaymentType PaymentType { get; set; }

        /// <summary>
        /// The status of the transaction
        /// </summary>
        public TransactionStatus Status { get; set; }
    }
}
