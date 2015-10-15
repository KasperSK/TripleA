﻿using System.Collections.Generic;

namespace CashRegister.Receipts
{
    /// <summary>
    /// A container object for a Receipt
    /// </summary>
	public class Receipt
	{
        public List<string> Content { get; private set; }

	    public Receipt()
	    {
	        Content = new List<string>();
	    }
	}
}