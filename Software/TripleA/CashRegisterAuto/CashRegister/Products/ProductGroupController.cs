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

	public class ProductGroupController : IProductGroupController
	{
		public virtual IGroupDao ProductGroupDao
		{
			get;
			set;
		}

		public virtual IProductController ProductController
		{
			get;
			set;
		}

		public virtual void GetProductGroupsByType(ProductGroupType type)
		{
			throw new System.NotImplementedException();
		}

	}
}
