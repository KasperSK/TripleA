using System;
using CashRegister.Models;

namespace CashRegister.Payment
{
    public class MobilePay : PaymentProvider
	{
        public override PaymentType Type => PaymentType.MobilePay;
        public override string Name => "MobilePay";
        public override string Description => "MobilePay from Danske Bank";
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
	}
}

