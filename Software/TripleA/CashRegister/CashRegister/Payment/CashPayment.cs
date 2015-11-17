﻿using CashRegister.Models;

namespace CashRegister.Payment
{
    /// <summary>
	/// The Cash drawers functionalities
	/// </summary>
	public class CashPayment : PaymentProvider
    {

        public CashPayment(int startChange)
	    {
	        _StartChange = startChange;
	    }

	    public override void Shutdown(){}

	    public override void Restart(){}

	    public override void Init()
	    {
	        
	    }

        public override PaymentType Type => PaymentType.Cash;

        public override bool TransferAmount(int amount, string desc)
	    {
	        Amount += amount;
	        return true;
	    }

	    public override bool TransactionStatus()
	    {
	        return true;
	    }
	}
}