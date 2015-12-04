using CashRegister.Printer;
using System;
using System.Globalization;
using CashRegister.Models;

namespace CashRegister.Receipts
{
    /// <summary>
    /// Implementation of a ReceiptController
    /// </summary>
    public class ReceiptController : IReceiptController
    {
        private readonly IFormatProvider _formatProvider;
        private IPrinter Printer { get; }

        public ReceiptController(IPrinter printer) : this(printer, CultureInfo.InvariantCulture)
        {
            
        }

        public ReceiptController(IPrinter printer, IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
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

            Printer.Print();
		}


        /// <summary>
        /// Formats and prints a new receipt from an order
        /// </summary>
        /// <param name="order">The Order to be formatted</param>
        public virtual void CreateReceipt(SalesOrder order)
		{
            var receipt = new Receipt(_formatProvider);

            CreateHeader(receipt, order.Id);
            
            foreach (var p in order.Lines)
            {
                receipt.Add(string.Format(_formatProvider, "{0}x\t{1}\n", p.Quantity, p.Product.Name));
                receipt.Add("              ");
                receipt.Add(string.Format(_formatProvider, "{0}\n\n", p.UnitPrice));
            }

            receipt.AddLine(string.Format(_formatProvider, "Total: {0}\n\n", order.Total));

            CreateFooter(receipt);

            Print(receipt);
		}

        /// <summary>
        /// Formats and prints a new receipt from a transaction
        /// </summary>
        /// <param name="transaction">The transaction to be formatted</param>
        public virtual void CreateReceipt(Transaction transaction)
        {
            var receipt = new Receipt(_formatProvider);
            
            receipt.Add(string.Format(_formatProvider, "{0}\nDate: {1}\nId: {2}\n{3}\n\b", transaction.PaymentType, transaction.Date, transaction.Id, transaction.Price));
            
            Print(receipt);
        }
        
        /// <summary>
        /// Adds the standard header to the Receipt
        /// </summary>
        /// <param name="receipt">The Receipt that is being formatted</param>
        /// <param name="orderId">The OrderId from the Order being formatted</param>
        private void CreateHeader(Receipt receipt, object orderId)
        {
            //var currentDateAndTime = DateTime.Today.ToString("G");

	        receipt.Add("Katrines Kælder\nFinlandsgade 22\nDK-8200 Aarhus N\n\n");
            receipt.Add(string.Format(_formatProvider ,"Dato: {0}\n", DateTime.Today));
            receipt.Add(string.Format(_formatProvider, "Ordre id: {0}\n\n", orderId));
            receipt.Add("-----\n");
        }

        /// <summary>
        /// Adds the standard footer to the Receipt
        /// </summary>
        /// <param name="receipt">The Receipt that is being formatted</param>
        private static void CreateFooter(Receipt receipt)
        {
            receipt.Add("-----\n");
            receipt.Add("På gensyn\n\n");
        }
    }
}