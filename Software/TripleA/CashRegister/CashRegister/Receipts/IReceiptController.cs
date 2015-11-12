using CashRegister.Database;
using CashRegister.Models;

namespace CashRegister.Receipts
{
    /// <summary>
	/// Interface to a ReceiptController which generates a Receipt
	/// </summary>
	public interface IReceiptController 
	{
        /// <summary>
        /// Prints a receipt
        /// </summary>
        /// <param name="receipt">The Receipt to be printed</param>
		void Print(Receipt receipt);

        /// <summary>
        /// Formats a new receipt from an order
        /// </summary>
        /// <param name="order">The Order to be formatted</param>
        /// <returns>A formatted Receipt</returns>
        Receipt CreateReceipt(SalesOrder order);

        /// <summary>
        /// Formats a new receipt from a transaction
        /// </summary>
        /// <param name="transaction">The Transaction to be formatted</param>
        /// <returns></returns>
        Receipt CreateReceipt(Transaction transaction);
	}
}