using CashRegister.Models;

namespace CashRegister.Receipts
{
    /// <summary>
	/// Interface to a ReceiptController.
	/// Formats a Receipt and prints the Receipt.
	/// </summary>
	public interface IReceiptController 
	{
        /// <summary>
        /// Formats a new Reciept from an order and prints the Reciept.
        /// </summary>
        /// <param name="order">The Order to be formatted and printed.</param>
        void CreateReceipt(SalesOrder order);

        /// <summary>
        /// Formats a new Receipt from a Transaction.
        /// </summary>
        /// <param name="transaction">The Transaction to be formatted and printed.</param>
        void CreateReceipt(Transaction transaction);
	}
}