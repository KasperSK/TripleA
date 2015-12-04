using log4net;

namespace CashRegister.Log
{
    /// <summary>
    /// Wrapper for log4net
    /// </summary>
    public class Logger : ILogger
	{
		private readonly ILog _log4Net;
        
		public void Warn(string line)
		{
			_log4Net.Warn(line);
		}

		public void Info(string line)
		{
			_log4Net.Info(line);
		}

		public void Err(string line)
		{
			_log4Net.Error(line);
		}

		public void Fatal(string line)
		{
			_log4Net.Fatal(line);
		}

		public void Debug(string line)
		{
			_log4Net.Debug(line);
		}

        public Logger(ILog loggerAdapter)
        {
            _log4Net = loggerAdapter;
        }

	}
}

