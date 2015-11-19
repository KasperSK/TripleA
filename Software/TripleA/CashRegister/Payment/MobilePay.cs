using System;
using System.Diagnostics.CodeAnalysis;
using CashRegister.Models;

namespace CashRegister.Payment
{
    [ExcludeFromCodeCoverage]
    public class MobilePay : PaymentProvider
	{
        public override PaymentType Type => PaymentType.MobilePay;
        public override string Name => "MobilePay";
        public override string Description => "MobilePay from Danske Bank";
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
    }
}

