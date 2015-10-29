using System.Collections.Generic;
using System.Linq;
using CashRegister.Models;

namespace CashRegister.Orders
{
    /// <summary>
    /// Implementation of the OrderController
    /// </summary>
	public class OrderController : IOrderController
	{
		private IOrderDao OrderDao { get; }

	    public OrderController (IOrderDao orderDao)
	    {
	        OrderDao = orderDao;
	    }

        /// <summary>
        /// Creates an order with an unique id
        /// </summary>
        /// <returns>The created SalesOrder</returns>
        public virtual SalesOrder CreateOrder()
        { 
            OrderDao.Insert(new SalesOrder());
            var order = OrderDao.GetLastOrder();

            return order;
		}

        /// <summary>
        /// Saves an order
        /// </summary>
        /// <param name="order">The order to be saved</param>
        public void SaveOrder(SalesOrder order)
	    {
	        OrderDao.Insert(order);
	    }

        /// <summary>
        /// Clears an order
        /// </summary>
        /// <param name="order">The order to be cleared</param>
        public void ClearOrder(ref SalesOrder order)
	    {
            order.Transactions.Clear();

            OrderDao.Update(order);
	    }

        /// <summary>
        /// Get the missing amount on an order
        /// </summary>
        /// <param name="order">The order</param>
        /// <returns>The missing amount</returns>
	    public long MissingAmount(SalesOrder order)
	    {
	        var amountLeft = order.Total - order.Transactions.Sum(t => t.Price);

	        return (long)amountLeft;
	    }

        /// <summary>
        /// Get an order by id
        /// </summary>
        /// <param name="id">The orders' id</param>
        /// <returns>The SalesOrder with id</returns>
        public virtual SalesOrder GetOrderById(long id)
		{
		   return OrderDao.SelectById(id);
		}

        /// <summary>
        /// Get the last n orders
        /// </summary>
        /// <param name="n">The amount of orders to be returned</param>
        /// <returns>A IEnumerable list with n amount of SalesOrders</returns>
        public virtual IEnumerable<SalesOrder> GetNLastOrders(int n)
		{
		    return OrderDao.GetNLastOrders(n);
		}
	}
}