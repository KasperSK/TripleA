using CashRegister.Printer;
using System;
using System.Globalization;
using CashRegister.Models;

namespace CashRegister.Receipts
{
    /// <summary>
    /// Implementation of a ReceiptController.
    /// Formats a Receipt and prints the Receipt.
    /// </summary>
    public class ReceiptController : IReceiptController
    {
        /// <summary>
        /// A IFormatProvider implementation for formatting the strings being processed by the class.
        /// </summary>
        private readonly IFormatProvider _formatProvider;

        /// <summary>
        /// The IPrinter implementation for printing the receipts. 
        /// </summary>
        private IPrinter Printer { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="printer">A IPrinter implementation for printing reciepts.</param>
        public ReceiptController(IPrinter printer) : this(printer, CultureInfo.InvariantCulture)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="printer">A IPrinter implementation for printing reciepts.</param>
        /// <param name="formatProvider">A IFormatProvider implementation for formatting the strings processed by the class.</param>
        public ReceiptController(IPrinter printer, IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
            Printer = printer;
        }

        /// <summary>
        /// Prints a Receipt.
        /// </summary>
        /// <param name="receipt">The Receipt to be printed.</param>
        private void Print(Receipt receipt)
		{
		    foreach (var line in receipt.Content)
		    {
		        Printer.AddLine(line);
		    }

            Printer.Print();
		}

        /// <summary>
        /// Formats a new Reciept from an order and prints the Reciept.
        /// </summary>
        /// <param name="order">The Order to be formatted and printed.</param>
        public virtual void CreateReceipt(SalesOrder order)
		{
            var receipt = new Receipt(_formatProvider);

            CreateHeader(receipt, order.Id);
            
            foreach (var p in order.Lines)
            {
                receipt.AddLine(string.Format(_formatProvider, "{0}x\t{1}\n", p.Quantity, p.Product.Name));
                receipt.AddLine("              ");
                receipt.AddLine(string.Format(_formatProvider, "{0}\n\n", p.UnitPrice));
            }

            receipt.AddLine(string.Format(_formatProvider, "Total: {0}\n\n", order.Total));

            CreateFooter(receipt);

            Print(receipt);
		}

        /// <summary>
        /// Formats a new Receipt from a Transaction.
        /// </summary>
        /// <param name="transaction">The Transaction to be formatted and printed.</param>
        public virtual void CreateReceipt(Transaction transaction)
        {
            var receipt = new Receipt(_formatProvider);
            
            receipt.AddLine(string.Format(_formatProvider, "{0}\nDate: {1}\nId: {2}\n{3}\n\b", transaction.PaymentType, transaction.Date, transaction.Id, transaction.Price));
            
            Print(receipt);
        }
        
        /// <summary>
        /// Adds the standard header to the Receipt.
        /// </summary>
        /// <param name="receipt">The Receipt that is being formatted.</param>
        /// <param name="orderId">The OrderId from the Order being formatted.</param>
        private void CreateHeader(Receipt receipt, object orderId)
        {
            //var currentDateAndTime = DateTime.Today.ToString("G");

	        receipt.AddLine("Katrines Kælder\nFinlandsgade 22\nDK-8200 Aarhus N\n\n");
            receipt.AddLine(string.Format(_formatProvider ,"Dato: {0}\n", DateTime.Today));
            receipt.AddLine(string.Format(_formatProvider, "Ordre id: {0}\n\n", orderId));
            receipt.AddLine("-----\n");
        }

        /// <summary>
        /// Adds the standard footer to the Receipt.
        /// </summary>
        /// <param name="receipt">The Receipt that is being formatted.</param>
        private static void CreateFooter(Receipt receipt)
        {
            receipt.AddLine("-----\n");
            receipt.AddLine("På gensyn\n\n");
        }
    }
}