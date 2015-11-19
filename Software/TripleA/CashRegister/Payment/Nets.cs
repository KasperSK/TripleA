using System;
using System.Diagnostics.CodeAnalysis;
using CashRegister.Models;

namespace CashRegister.Payment
{
    [ExcludeFromCodeCoverage]
    public class Nets : PaymentProvider
	{
		public virtual void GetDescriptor()
		{
			throw new System.NotImplementedException();
		}

	    public override void Init(){}

	    public override bool TransferAmount(int amount, string description)
	    {
	        return false;
	    }

	    public override bool TransactionStatus()
	    {
	        return false;
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
	        return 0;
	    }

	    public override PaymentType Type => PaymentType.Nets;
        public override string Name => "Nets";
        public override string Description => "Nets Dankort/Visa";
	}
}
