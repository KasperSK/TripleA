﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace CashRegister.Orders
{
	using CashRegister.Database;
	using CashRegister.Products;
	using CashRegister.ShoppingLists;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public interface IOrderController 
	{
		void CreateOrder(ShoppingList shoppinglist);

		Order GetOrderByID(int id);

		/// <param name="n">How many orders we wish to ha returned</param>
		void GetNLastOrders(int n);

	}
}
