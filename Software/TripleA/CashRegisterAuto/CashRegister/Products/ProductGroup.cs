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

	public class ProductGroup
	{
		public virtual object Name
		{
			get;
			set;
		}

		public virtual object ID
		{
			get;
			set;
		}

		public virtual IEnumerable<Product> Products
		{
			get;
			set;
		}

		public virtual ProductGroupType Type
		{
			get;
			set;
		}

	}
}

