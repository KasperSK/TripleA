
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CashRegister.Receipts
{
    /// <summary>
    /// A container object for a Receipt
    /// </summary>
	public class Receipt
	{
        public IReadOnlyCollection<string> Content => _contentList.ToList();
        private readonly IList<string> _contentList;  
        private readonly IFormatProvider _formatProvider;

        public Receipt() : this(CultureInfo.InvariantCulture)
        {
            
        }

        public Receipt(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
	        _contentList = new List<string>();
	    }

        public void Add(string line)
        {
            _contentList.Add(string.Format(_formatProvider, line));
        }

        public void AddLine(string formattable)
        {
            Add(formattable.ToString(_formatProvider));
        }
	}
}