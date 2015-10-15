using CashRegister.Orders;
using CashRegister.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashRegister.Receipts
{
    /// <summary>
	/// Interface to a ReceiptController which generates a Receipt
	/// </summary>
	public interface IReceiptController 
	{
		void Print(Receipt receipt);
        Receipt CreateReceipt(Order order);
	}
}