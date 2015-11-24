using System.Collections.Generic;
using CashRegister.Database;
using CashRegister.Models;

namespace CashRegister.Orders
{
    /// <summary>
    /// Interface to the OrderController
    /// </summary>
	public interface IOrderController 
	{
        SalesOrder CurrentOrder { get;}

        IReadOnlyCollection<SalesOrder> StashedOrders { get; }

        /// <summary>
        /// Saves the current order.
        /// </summary>
        void SaveOrder();

        /// <summary>
        /// Clears the order lines from the current order.
        /// </summary>
        void ClearOrder();

        /// <summary>
        /// Adds an amount of products to the current order.
        /// </summary>
        /// <param name="product">Product to be added to the order</param>
        /// <param name="quantity">The quantity of the product</param>
        /// <param name="discount">The discount, can be excluded</param>
        void AddProduct(Product product);
        void AddProduct(Product product, int quantity);
        void AddProduct(Product product, int quantity, Discount discount);

        void StashCurrentOrder();

        /// <summary>
        /// Retrieves a stashed order
        /// </summary>
        /// <param name="id">The internal id of the stashed item</param>
        void GetStashedOrder(int id);
        
        /// <summary>
        /// Get the missing amount from the current order
        /// </summary>
        /// <returns>The missing amount</returns>
	    long MissingAmount();

        /// <summary>
        /// Get an order by id and sets it as the current order
        /// </summary>
        /// <param name="id">The orders' id</param>
        void GetOrderById(long id);

		/// <summary>
		/// Get the last n orders and updates the currents orders
		/// </summary>
		/// <param name="amount">The amount of orders to be returned</param>
		IEnumerable<SalesOrder> GetLastOrders(int amount);
	}
}