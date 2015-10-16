using System;

namespace CashRegister.Payment
{
    public class Nets : IPaymentProvider
	{
		public virtual void GetDescriptor()
		{
			throw new System.NotImplementedException();
		}

	    public void Init(){}

	    public bool TransferAmount(int amount, string desc)
	    {
	        throw new NotImplementedException();
	    }

	    public bool TransactionStatus()
	    {
	        throw new NotImplementedException();
	    }

	    public void Restart()
	    {
	        throw new NotImplementedException();
	    }

	    public void Shutdown()
	    {
	        throw new NotImplementedException();
	    }

	    public int Tally()
	    {
	        throw new NotImplementedException();
	    }

	    public int ID { get; set; }
	    public string Name { get; set; }
	    public string Description { get; set; }
	}
}