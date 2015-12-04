using System.Collections.Generic;
using CashRegister.Models;

namespace CashRegister.Products
{
	/// <summary>
	/// Interface to a Product Controller
	/// This controller is used to retrive products for the Cashregister
	/// </summary>
    public interface IProductController
    {
		/// <summary>
        /// Collection of the ProductTabs that are active and have Products that are saleable
        /// </summary>
        IReadOnlyCollection<ProductTab> ProductTabs { get; }
    }
}