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
		/// <summary>
		/// Creates an order with an unique id
		/// </summary>
		/// <returns>The created SalesOrder</returns>
		SalesOrder CreateOrder();

        /// <summary>
        /// Saves an order
        /// </summary>
        /// <param name="order">The order to be saved</param>
        void SaveOrder(SalesOrder order);

        /// <summary>
        /// Clears an order
        /// </summary>
        /// <param name="order">The order to be cleared</param>
        void ClearOrder(ref SalesOrder order);

        /// <summary>
        /// Get the missing amount on an order
        /// </summary>
        /// <param name="order">The order</param>
        /// <returns>The missing amount</returns>
	    long MissingAmount(SalesOrder order);

        /// <summary>
        /// Get an order by id
        /// </summary>
        /// <param name="id">The orders' id</param>
        /// <returns>The SalesOrder with id</returns>
        SalesOrder GetOrderById(long id);

		/// <summary>
		/// Get the last n orders
		/// </summary>
		/// <param name="n">The amount of orders to be returned</param>
		/// <returns>A IEnumerable list with n amount of SalesOrders</returns>
		IEnumerable<SalesOrder> GetNLastOrders(int n);
	}
}