using System.Collections.Generic;
using System.ComponentModel;
using CashRegister.Models;

namespace CashRegister.Orders
{
    /// <summary>
    /// Interface to the OrderController
    /// This controller handles SalesOrders for the CashRegister.
    /// </summary>
	public interface IOrderController : INotifyPropertyChanged
    {
        /// <summary>
        /// Returns the current order.
        /// </summary>
        SalesOrder CurrentOrder { get;}

        /// <summary>
        /// Returns a collection of the stashed orders.
        /// </summary>
        IReadOnlyCollection<SalesOrder> StashedOrders { get; }

        /// <summary>
        /// Saves the current SalesOrder.
        /// </summary>
        void SaveOrder();

        /// <summary>
        /// Clears the OrderLines from the current SalesOrder.
        /// </summary>
        void ClearOrder();

        /// <summary>
        /// Adds an amount of products to the current SalesOrder.
        /// </summary>
        /// <param name="product">Product to be added to the SalesOrder.</param>
        void AddProduct(Product product);

        /// <summary>
        /// Adds an amount of products to the SalesOrder.
        /// </summary>
        /// <param name="product">Product to be added to the SalesOrder.</param>
        /// <param name="quantity">The quantity of the Product. Can be negative.</param>
        void AddProduct(Product product, int quantity);

        /// <summary>
        /// Adds an amount of products to the SalesOrder.
        /// </summary>
        /// <param name="product">Product to be added to the SalesOrder.</param>
        /// <param name="quantity">The quantity of the Product. Can be negative.</param>
        /// <param name="discount">The discount on the product. Can be null.</param>
        void AddProduct(Product product, int quantity, Discount discount);

        /// <summary>
        /// Stashes the current SalesOrder.
        /// </summary>
        void StashCurrentOrder();

        /// <summary>
        /// Retrieves a stashed SalesOrder.
        /// </summary>
        /// <param name="id">The internal id of the stashed SalesOrder</param>
        void GetStashedOrder(int id);
        
        /// <summary>
        /// Get the missing amount from the current SalesOrder.
        /// </summary>
        /// <returns>The missing amount on the current SalesOrder.</returns>
	    long MissingAmount();

        /// <summary>
        /// Get a SalesOrder by id and set it as the CurrentOrder.
        /// </summary>
        /// <param name="id">The wanted SalesOrder id.</param>
        void GetOrderById(long id);

        /// <summary>
        /// Gets the last n SalesOrder
        /// </summary>
        /// <param name="amount">The amount of orders to be returned</param>
		IEnumerable<SalesOrder> GetLastOrders(int amount);
	}
}