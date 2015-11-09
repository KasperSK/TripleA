namespace CashRegister.Payment
{
    public interface ITransactionObserver 
	{
		void PaymentCompleted(ObservableTransaction transaction);

		void PaymentFailed(ObservableTransaction transaction);

	}
}