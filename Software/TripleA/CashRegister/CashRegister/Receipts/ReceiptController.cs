using CashRegister.Orders;
using CashRegister.Printer;
using System;
using CashRegister.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashRegister.Receipts
{
    /// <summary>
    /// Implementation of a ReceiptController which generates a Reciept
    /// </summary>
    public class ReceiptController : IReceiptController
	{
        private IPrinter Printer { get; }

        public ReceiptController(IPrinter printer)
        {
            Printer = printer;
        }

        /// <summary>
        /// Initiates a Print of a Receipt
        /// </summary>
        public virtual void Print(Receipt receipt)
		{
		    foreach (var line in receipt.Content)
		    {
		        Printer.AddTo(line);
		    }
		}

        /// <summary>
        /// Creates a Receipt with information from an Order object
        /// </summary>
        public virtual Receipt CreateReceipt(OrderList order)
		{
            var receipt = new Receipt();

            CreateHeader(receipt, order.OrderId);
            
            foreach (var o in order.Products)
            {
                var p = o;

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
        private static void CreateFooter(Receipt receipt)
        {
            receipt.Content.Add("-----\n");
            receipt.Content.Add("På gensyn\n\n");
        }
    }
}