﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace CashRegister.Products
{
	using CashRegister.Database;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Controller Interface to manage Products, the controller is the only one that can create a Product. It will assing a unique ID to the Product. 
	/// </summary>
	public interface IProductController 
	{
		/// <summary>
		/// The only way to create a new Product, It will create the Product with the name given and a new unique ID
		/// </summary>
		/// <param name="name">The name of the new Product, does not have to be unique</param>
		Product CreateProduct(string name);

		/// <summary>
		/// Returns a List of Products that matches the given ProductGroup ex. ActiveProducts
		/// </summary>
		/// <param name="productgroup">The ProductGroup to find alle Products of</param>
		IEnumerable<Product> GetProductsByGroup(int productgroup);

	}
}

