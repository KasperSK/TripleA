using System.Collections.Generic;
using CashRegister.Database;

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
		/// <returns>The created OrderList</returns>
		OrderList CreateOrder();

        /// <summary>
        /// Saves an order
        /// </summary>
        /// <param name="order">The order to be saved</param>
        void SaveOrder(OrderList order);

        /// <summary>
        /// Clears an order
        /// </summary>
        /// <param name="order">The order to be cleared</param>
        void ClearOrder(ref OrderList order);

        /// <summary>
        /// Get the missing amount on an order
        /// </summary>
        /// <param name="order">The order</param>
        /// <returns>The missing amount</returns>
	    long MissingAmount(OrderList order);

        /// <summary>
        /// Get an order by id
        /// </summary>
        /// <param name="id">The orders' id</param>
        /// <returns>The OrderList with id</returns>
        OrderList GetOrderById(long id);

		/// <summary>
		/// Get the last n orders
		/// </summary>
		/// <param name="n">The amount of orders to be returned</param>
		/// <returns>A IEnumerable list with n amount of OrderLists</returns>
		IEnumerable<OrderList> GetNLastOrders(int n);
	}
}