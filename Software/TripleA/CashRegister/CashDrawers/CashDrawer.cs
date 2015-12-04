using System.Diagnostics.CodeAnalysis;
using CashRegister.Log;

namespace CashRegister.CashDrawers
{
    /// <summary>
    /// Implementation of the CashDrawer
    /// </summary>
    public class CashDrawer : ICashDrawer
	{
        private readonly ILogger _logger = LogFactory.GetLogger(typeof(CashDrawer));

        public CashDrawer(int startChange)
        {
            CashChange = startChange;
        }
		/// <summary>
		/// Opens the CashDrawer
		/// </summary>
		[ExcludeFromCodeCoverage]
		public void Open()
		{
		    _logger.Debug("Open");
		}

        public int CashChange { get; }
	}
}