using System.Collections.Generic;
using CashRegister.Models;

namespace CashRegister.Payment
{
    public interface IPaymentController 
	{
		IEnumerable<IPaymentProvidorDescriptor> PaymentProvidorDescriptors { get; }

	    bool ExecuteTransaction(Transaction transaction);
        
        void PrintTransaction(Transaction transaction);
        
	    int Tally();

	}
}