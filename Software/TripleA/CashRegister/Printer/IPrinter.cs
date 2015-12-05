namespace CashRegister.Printer
{
    /// <summary>
    /// Interface to a printing device.
    /// Takes lines of string and prints the strings.
    /// </summary>
    public interface IPrinter 
	{
        /// <summary>
        /// Adds a string to the print document.
        /// </summary>
        /// <param name="line">line is added to the print document.</param>
        void AddLine(string line);

		/// <summary>
		/// Initiates a print.
		/// </summary>
		void Print();
	}
}