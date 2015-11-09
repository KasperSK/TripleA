using System.Collections.Generic;

namespace CashRegister.Payment
{
    public interface IPaymentController 
	{
		IEnumerable<IPaymentProvidorDescriptor> PaymentProvidorDescriptors { get; }

	    void ExecuteTransaction(ITransaction transaction, IPaymentProvidorDescriptor PD);

        void PrintTransaction(ITransaction transaction);

	    int Tally();

	}
}