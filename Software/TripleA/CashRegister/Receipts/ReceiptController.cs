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
		}


        /// <summary>
        /// Formats a new receipt from an order
        /// </summary>
        /// <param name="order">The Order to be formatted</param>
        /// <returns>A formatted Receipt</returns>
        public virtual Receipt CreateReceipt(SalesOrder order)
		{
            var receipt = new Receipt(_formatProvider);

            CreateHeader(receipt, order.Id);
            
            foreach (var p in order.Lines)
            {
                receipt.AddLine($"{p.Quantity}x\t{p.Product.Name}\n");
                receipt.Add("              ");
                receipt.AddLine($"{p.UnitPrice}\n\n");
            }

            receipt.AddLine($"Total: {order.Total}\n\n");

            CreateFooter(receipt);

            return receipt;
		}

        /// <summary>
        /// Formats a new receipt from a transaction
        /// </summary>
        /// <param name="transaction">The transaction to be formatted</param>
        /// <returns></returns>
        public virtual Receipt CreateReceipt(Transaction transaction)
        {
            var receipt = new Receipt(_formatProvider);
            
            receipt.AddLine($"{transaction.PaymentType}\nDate: {transaction.Date}\nId: {transaction.Id}\n{transaction.Price}\n\b");

            return receipt;
        }
        
        /// <summary>
        /// Adds the standard header to the Receipt
        /// </summary>
        /// <param name="receipt">The Receipt that is being formatted</param>
        /// <param name="orderId">The OrderId from the Order being formatted</param>
        private static void CreateHeader(Receipt receipt, object orderId)
        {
            //var currentDateAndTime = DateTime.Today.ToString("G");

	        receipt.Add("Katrines Kælder\nFinlandsgade 22\nDK-8200 Aarhus N\n\n");
            receipt.AddLine($"Dato: {DateTime.Today}\n");
            receipt.AddLine($"Ordre id: {orderId}\n\n");
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