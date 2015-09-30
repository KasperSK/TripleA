﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace CashRegister.Log
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

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
		void Error(string line);

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

