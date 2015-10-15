using CashRegister.Orders;
using CashRegister.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashRegister.Receipts
{
	public class Receipt
	{
        /// <summary>
        /// A list of strings with the content of the Receipt
        /// </summary>
        public List<string> Content { get; private set; }

	    public Receipt()
	    {
	        Content = new List<string>();
	    }
	}
}