﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace CashRegister.Sales
{
	using CashRegister.Orders;
	using CashRegister.Payment;
	using CashRegister.Products;
	using CashRegister.Receipts;
	using CashRegister.ShoppingLists;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public interface ISalesController 
	{
		void AddProductToShoppingList(Product product);

		void BuyShoppingList();

		void RemoveProductFromShoppingList(Product product);

		void ClearShoppingList();

	}
}

