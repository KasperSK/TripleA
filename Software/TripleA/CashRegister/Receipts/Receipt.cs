using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CashRegister.Receipts
{
    /// <summary>
    /// A container object for a Receipt.
    /// </summary>
	public class Receipt
	{
        /// <summary>
        /// A read-only collection of the strings in the Receipt.
        /// </summary>
        public IReadOnlyCollection<string> Content => _contentList.ToList();

        /// <summary>
        /// A collection of the strings in the Receipt.
        /// </summary>
        private readonly IList<string> _contentList;

        /// <summary>
        /// A IFormatProvider implementation for formatting the strings being processed by the class.
        /// </summary>
        private readonly IFormatProvider _formatProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        public Receipt() : this(CultureInfo.InvariantCulture)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="formatProvider">A IFormatProvider implementation for formatting the strings processed by the class.</param>
        public Receipt(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
	        _contentList = new List<string>();
	    }

        /// <summary>
        /// Adds a line to the Receipt.
        /// </summary>
        /// <param name="line"></param>
        public void AddLine(string line)
        {
            _contentList.Add(string.Format(_formatProvider, line));
        }
    }
}