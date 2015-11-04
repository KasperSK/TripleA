using System.Collections.Generic;
using CashRegister.Models;

namespace CashRegister.Payment
{	
	public class ObservableTransaction : ITransaction
	{
	    private Transaction _transaction;
	    
		public IPaymentProvidorDescriptor PaymentDescriptor { get; set; }

	    public int Amount { get; set; }
	    public string Description { get; set; }
	    public int ID { get; }
	    IEnumerable<ITransactionObserver> ITransaction.Observers
	    {
	        get { return Observers; }
	        set { Observers = value; }
	    }

	    public TransactionStatus Status { get; set; }
	    public IEnumerable<ITransactionObserver> Observers { get; set; }

	    public virtual void AddObserver(ITransactionObserver observer)
		{
			throw new System.NotImplementedException();
		}
	}
}