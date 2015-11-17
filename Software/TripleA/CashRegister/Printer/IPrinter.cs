namespace CashRegister.Printer
{
	/// <summary>
	/// Interface to a printing device
	/// </summary>
	public interface IPrinter 
	{
        /// <summary>
        /// Adds a string to the print document
        /// </summary>
        /// <param name="line">line is added to the print document</param>
        void AddTo(string line);

		/// <summary>
		/// Initiates a print
		/// </summary>
		void Print();
	}
}