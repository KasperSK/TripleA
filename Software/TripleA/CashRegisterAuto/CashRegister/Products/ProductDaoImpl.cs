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
	/// Implementation og IProductDao
	/// </summary>
	public class ProductDaoImpl : IProductDao
	{
		public virtual IDatabase Database
		{
			get;
			set;
		}

		public virtual void Insert(Product product)
		{
			throw new System.NotImplementedException();
		}

		public virtual Product SelectByID(int id)
		{
			throw new System.NotImplementedException();
		}

		public virtual void Delete(Product product)
		{
			throw new System.NotImplementedException();
		}

		public virtual void Update(Product product)
		{
			throw new System.NotImplementedException();
		}

		public virtual Product SelectByGroupID(int id)
		{
			throw new System.NotImplementedException();
		}

	}
}

