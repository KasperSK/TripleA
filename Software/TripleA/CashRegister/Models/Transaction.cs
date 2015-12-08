using System.Diagnostics.CodeAnalysis;

namespace CashRegister.Models
{
    using System;

    /// <summary>
    /// The status of the Transaction
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// Transaction is created and saved in the database
        /// </summary>
        Created = 0,

        /// <summary>
        /// Transaction is completed and payed
        /// </summary>
        Completed = 1,

        /// <summary>
        /// Transaction is aborted or other error has occured
        /// </summary>
        Failed = 2,
    }

    /// <summary>
    /// Which kind of PaymentType has been used to pay an transaction
    /// </summary>
    public enum PaymentType
    {
        /// <summary>
        /// Cash - Yes physical money
        /// </summary>
        Cash = 0,

        /// <summary>
        /// Nets - Handles Dankort, Visa and other cards
        /// </summary>
        Nets = 1,

        /// <summary>
        /// Swipp - Handles swipp payments.
        /// </summary>
        Swipp = 2,

        /// <summary>
        /// MobilePay - Handles payment via mobilepay
        /// </summary>
        MobilePay = 3,
    }

    /// <summary>
    /// Is used to store and transport transactions around the system
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Transaction
    {
        /// <summary>
        /// Unique Transaction Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Should contain SalesOrders Id, and is used to forward to the PaymentProvider
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Date and time of transaction
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// How much have / shall be transfered
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// The SalesOrder, this transaction are linked to
        /// </summary>
        public virtual SalesOrder SalesOrder { get; set; }

        /// <summary>
        /// Which PaymentType was used, this is to store in the Database
        /// </summary>
        public PaymentType PaymentType { get; set; }

        /// <summary>
        /// The TransactionStatus of the Transaction (see TransactionStatus)
        /// </summary>
        public TransactionStatus Status { get; set; }
    }
}
