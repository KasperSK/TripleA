using System.Collections.Generic;

namespace CashRegister.Receipts
{
    /// <summary>
    /// A container object for a Receipt
    /// </summary>
	public class Receipt
	{
        public ICollection<string> Content { get; }

	    public Receipt()
	    {
	        Content = new List<string>();
	    }
	}
}