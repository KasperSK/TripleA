using System;
using CashRegister.Models;

namespace CashRegister.Payment
{
    public class Nets : PaymentProvider
	{
		public virtual void GetDescriptor()
		{
			throw new System.NotImplementedException();
		}

	    public override void Init(){}

	    public override bool TransferAmount(int amount, string description)
	    {
	        throw new NotImplementedException();
	    }

	    public override bool TransactionStatus()
	    {
	        throw new NotImplementedException();
	    }

	    public override void Restart()
	    {
	        throw new NotImplementedException();
	    }

	    public override void Shutdown()
	    {
	        throw new NotImplementedException();
	    }

	    public override int Tally()
	    {
	        throw new NotImplementedException();
	    }

	    public override PaymentType Type => PaymentType.Nets;
        public override string Name => "Nets";
        public override string Description => "Nets Dankort/Visa";
	}
}
