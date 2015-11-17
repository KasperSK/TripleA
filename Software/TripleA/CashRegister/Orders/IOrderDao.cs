using System.Collections.Generic;
using CashRegister.Models;

namespace CashRegister.Orders
{
    /// <summary>
    /// Interface to Data Access Objects for Orders
    /// </summary>
	public interface IOrderDao 
	{
		/// <summary>
		/// Delete an order in the database
		/// </summary>
		/// <param name="order">The order to be deleted</param>
		void Delete(SalesOrder order);

		/// <summary>
		/// Update an order in the database
		/// </summary>
		/// <param name="order">The order to be updated</param>
		void Update(SalesOrder order);

		/// <summary>
		/// Insert an order in to the database
		/// </summary>
		/// <param name="order">The order to be inserted</param>
		void Insert(SalesOrder order);

        /// <summary>
        /// Get an order from id
        /// </summary>
        /// <param name="id">The id of the order</param>
        /// <returns>An SalesOrder from an id</returns>
        SalesOrder SelectById(long id);

        /// <summary>
        /// Get a list of the last n orders
        /// </summary>
        /// <param name="amount">The amount of orders to be returned</param>
        /// <returns>A IEnumerable list of the last n orders</returns>
        IEnumerable<SalesOrder> GetLastOrders(int amount);

        /// <summary>
        /// Get the last order
        /// </summary>
        /// <returns>Returns the latest order</returns>
        SalesOrder LastOrder { get; }

        /// <summary>
        /// Get the id of the last order
        /// </summary>
        /// <returns>The id of the last order</returns>
        long LastId { get; }
    }
}