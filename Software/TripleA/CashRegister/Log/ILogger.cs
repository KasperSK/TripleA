namespace CashRegister.Log
{
    /// <summary>
    /// Logger interface to easy filter on logs
    /// </summary>
    public interface ILogger 
	{
		/// <summary>
		/// To use for lines of the severity warn
		/// </summary>
		void Warn(string line);

		/// <summary>
		/// To use for lines of the severity info
		/// </summary>
		void Info(string line);

		/// <summary>
		/// To use for lines of the severity error
		/// </summary>
		void Err(string line);

		/// <summary>
		/// To use for lines of the severity fatal
		/// </summary>
		void Fatal(string line);

		/// <summary>
		/// To use for lines of the severity debug
		/// </summary>
		void Debug(string line);
	}
}

