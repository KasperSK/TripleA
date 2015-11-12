using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Payment
{
    public interface ITransactionObserver : IObservable<List<ITransactionObserver>>
    {
		void PaymentCompleted(ObservableTransaction transaction);

		void PaymentFailed(ObservableTransaction transaction);

	}
}