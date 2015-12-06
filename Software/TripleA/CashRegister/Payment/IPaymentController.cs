using System.Collections.Generic;
using CashRegister.Models;

namespace CashRegister.Payment
{
    /// <summary>
    /// Interface for Payment controllers
    /// </summary>
    public interface IPaymentController 
	{
        /// <summary>
        /// To return a list of payment providors
        /// </summary>
		IEnumerable<IPaymentProviderDescriptor> PaymentProviderDescriptors { get; }

        /// <summary>
        /// To execute a transaction
        /// </summary>
        /// <param name="transaction">The transaction to be executet</param>
        /// <returns>Returns how the execution went</returns>
	    bool ExecuteTransaction(Transaction transaction);
        
        /// <summary>
        /// To print a transaction 
        /// </summary>
        /// <param name="transaction">The transaction to be printet</param>
        void PrintTransaction(Transaction transaction);
        
        /// <summary>
        /// To account for all the transactions up to this point
        /// </summary>
        /// <returns></returns>
	    string Tally();

	}
}