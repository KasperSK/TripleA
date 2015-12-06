using System.Collections.Generic;
using CashRegister.Models;

namespace CashRegister.Orders
{
    /// <summary>
    /// Interface to SalesOrder DataAccessObjects.
    /// This controls how we access the SalesOrder and OrderLines in the database.
    /// </summary>
	public interface IOrderDao 
	{
        /// <summary>
        /// Delete an SalesOrder in the database.
        /// </summary>
        /// <param name="order">The SalesOrder to be deleted.</param>
        void Delete(SalesOrder order);

        /// <summary>
        /// Update a SalesOrder in the database.
        /// </summary>
        /// <param name="order">The SalesOrder to be updated.</param>
        void Update(SalesOrder order);

		/// <summary>
		/// Insert a SalesOrder in to the database.
		/// </summary>
		/// <param name="order">The SalesOrder to be inserted.</param>
		void Insert(SalesOrder order);

        /// <summary>
        /// Gets a SalesOrder from id.
        /// </summary>
        /// <param name="id">The id of the SalesOrder.</param>
        /// <returns>A SalesOrder from an id.</returns>
        SalesOrder SelectById(long id);

        /// <summary>
        /// Get a list of the last n SalesOrder's.
        /// </summary>
        /// <param name="amount">The amount of SalesOrder's to be returned.</param>
        /// <returns>A IEnumerable list of the last n SalesOrder's.</returns>
        IEnumerable<SalesOrder> GetLastOrders(int amount);

        /// <summary>
        /// Get the last SalesOrder.
        /// </summary>
        /// <returns>Returns the last SalesOrder.</returns>
        SalesOrder LastOrder { get; }

        /// <summary>
        /// Get the id of the last SalesOrder.
        /// </summary>
        /// <returns>The id of the last SalesOrder.</returns>
        long LastId { get; }

        /// <summary>
        /// Inserts an OrderLine into the database.
        /// </summary>
        /// <param name="line">The OrderLine to be inserted</param>
        void AddOrderLine(OrderLine line);

        /// <summary>
        /// Clears the orderlines from an excisting SalesOrder.
        /// </summary>
        /// <param name="order">The SalesOrder to be cleared.</param>
        void ClearOrder(SalesOrder order);
	}
}