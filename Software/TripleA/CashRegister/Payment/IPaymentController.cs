using System.Collections.Generic;
using CashRegister.Models;

namespace CashRegister.Payment
{
    public interface IPaymentController 
	{
		IEnumerable<IPaymentProviderDescriptor> PaymentProviderDescriptors { get; }

	    bool ExecuteTransaction(Transaction transaction);
        
        void PrintTransaction(Transaction transaction);
        
	    string Tally();

	}
}