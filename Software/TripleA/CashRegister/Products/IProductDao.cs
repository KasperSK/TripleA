using System.Collections.Generic;
using System.Collections.ObjectModel;
using CashRegister.Models;

namespace CashRegister.Products
{
	/// <summary>
	/// Interface to a Product Database Object
	/// This controls how we access the Products, ProductGroups and ProductTabs in the Database
	/// </summary>
    public interface IProductDao
    {
		/// <summary>
        /// Collection of the ProductTabs that are active and have Products that are saleable
        /// </summary>
        ReadOnlyCollection<ProductTab> GetProductTabs(bool onlyActive);
    }
}