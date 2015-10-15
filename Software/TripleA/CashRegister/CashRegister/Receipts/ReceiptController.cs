using CashRegister.Printer;
using System;
using CashRegister.Database;

namespace CashRegister.Receipts
{
    /// <summary>
    /// Implementation of a ReceiptController
    /// </summary>
    public class ReceiptController : IReceiptController
	{
        private IPrinter Printer { get; }

        public ReceiptController(IPrinter printer)
        {
            Printer = printer;
        }

        /// <summary>
        /// Prints a receipt
        /// </summary>
        /// <param name="receipt">The Receipt to be printed</param>
        public virtual void Print(Receipt receipt)
		{
		    foreach (var line in receipt.Content)
		    {
		        Printer.AddTo(line);
		    }
		}

        /// <summary>
        /// Formats a new receipt from an order
        /// </summary>
        /// <param name="order">The Order to be formatted</param>
        /// <returns>A formatted Receipt</returns>
        public virtual Receipt CreateReceipt(OrderList order)
		{
            var receipt = new Receipt();

            CreateHeader(receipt, order.OrderId);
            
            foreach (var p in order.Products)
            {
                receipt.Content.Add($"{p.ProductName}\n");
                receipt.Content.Add("              ");
                receipt.Content.Add($"{p.Prices}\n\n");
            }

            receipt.Content.Add($"Total: {order.OrderTotal}\n\n");

            CreateFooter(receipt);

            return receipt;
		}

        /// <summary>
        /// Adds the standard header to the Receipt
        /// </summary>
        /// <param name="receipt">The Receipt that is being formatted</param>
        /// <param name="orderId">The OrderId from the Order being formatted</param>
        private static void CreateHeader(Receipt receipt, object orderId)
        {
            var currentDateAndTime = DateTime.Today.ToString("G");

	        receipt.Content.Add("Katrines Kælder\nFinlandsgade 22\nDK-8200 Aarhus N\n\n");
            receipt.Content.Add($"Dato: {currentDateAndTime}\n");
            receipt.Content.Add($"Ordre id: {orderId}\n\n");
            receipt.Content.Add("-----\n");
        }

        /// <summary>
        /// Adds the standard footer to the Receipt
        /// </summary>
        /// <param name="receipt">The Receipt that is being formatted</param>
        private static void CreateFooter(Receipt receipt)
        {
            receipt.Content.Add("-----\n");
            receipt.Content.Add("På gensyn\n\n");
        }
    }
}