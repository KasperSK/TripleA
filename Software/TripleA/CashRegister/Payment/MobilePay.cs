using System;
using CashRegister.Models;

namespace CashRegister.Payment
{
    public class MobilePay : PaymentProvider
	{
        public override PaymentType Type => PaymentType.MobilePay;
        public override void Init(){}

        public override bool TransferAmount(int amount, string desc)
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
	}
}

