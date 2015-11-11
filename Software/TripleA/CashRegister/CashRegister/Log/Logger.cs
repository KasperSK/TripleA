
using log4net.Config;

namespace CashRegister.Log
{
    using log4net;

    /// <summary>
    /// Wrapper for log4net
    /// </summary>
    public class Logger : ILogger
	{
		private readonly ILog _log4Net;

        internal static void Configure()
        {
            BasicConfigurator.Configure();
        }
        
		public virtual void Warn(string line)
		{
			_log4Net.Warn(line);
		}

		public virtual void Info(string line)
		{
			_log4Net.Info(line);
		}

		public virtual void Err(string line)
		{
			_log4Net.Error(line);
		}

		public virtual void Fatal(string line)
		{
			_log4Net.Fatal(line);
		}

		public virtual void Debug(string line)
		{
			_log4Net.Debug(line);
		}

		internal Logger(System.Type type)
		{
		    _log4Net = LogManager.GetLogger(type);
		}

	}
}

