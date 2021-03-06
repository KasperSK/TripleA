﻿using System.Diagnostics.CodeAnalysis;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;

namespace CashRegister.Log
{
    [ExcludeFromCodeCoverage]
    public abstract class LogFactory
	{
		/// <summary>
		/// Returns a logger instance, use a static variable to hold it 
		/// </summary>
		/// <param name="type">Insert the class that is using the logger ex typeof(ProductController)</param>
		public static ILogger GetLogger(System.Type type)
		{
			return new Logger(LogManager.GetLogger(type));
		}

        /// <summary>
        /// Run once in start of the program. Is used to configure how the logger shall output the log
        /// </summary>
        public static void Configure(string fileName, bool append)
        {
            var layout = new PatternLayout("%date [%thread] %-5level %logger - %message %newline");
            layout.ActivateOptions();

            var fileappender = new FileAppender
            {
                AppendToFile = append,
                Encoding = Encoding.UTF8,
                File = fileName,
                Layout = layout
            };
            fileappender.ActivateOptions();

            BasicConfigurator.Configure(fileappender);
		}

	}
}

