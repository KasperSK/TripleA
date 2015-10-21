using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation.Peers;
using CashRegister.Database;

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
        /// <returns>The created OrderList</returns>
        public virtual OrderList CreateOrder()
        { 
            OrderDao.Insert(new OrderList());
            var order = OrderDao.GetLastOrder();

            return order;
		}

        /// <summary>
        /// Saves an order
        /// </summary>
        /// <param name="order">The order to be saved</param>
        public void SaveOrder(OrderList order)
	    {
	        OrderDao.Insert(order);
	    }

        /// <summary>
        /// Clears an order
        /// </summary>
        /// <param name="order">The order to be cleared</param>
        public void ClearOrder(ref OrderList order)
	    {
	        order.Products.Clear();
            order.Transaktions.Clear();

            OrderDao.Update(order);
	    }

        /// <summary>
        /// Get the missing amount on an order
        /// </summary>
        /// <param name="order">The order</param>
        /// <returns>The missing amount</returns>
	    public long MissingAmount(OrderList order)
	    {
	        var amountLeft = order.OrderTotal - order.Transaktions.Sum(t => t.TransaktionPrice);

	        return (long)amountLeft;
	    }

        /// <summary>
        /// Get an order by id
        /// </summary>
        /// <param name="id">The orders' id</param>
        /// <returns>The OrderList with id</returns>
        public virtual OrderList GetOrderById(long id)
		{
		   return OrderDao.SelectById(id);
		}

        /// <summary>
        /// Get the last n orders
        /// </summary>
        /// <param name="n">The amount of orders to be returned</param>
        /// <returns>A IEnumerable list with n amount of OrderLists</returns>
        public virtual IEnumerable<OrderList> GetNLastOrders(int n)
		{
		    return OrderDao.GetNLastOrders(n);
		}
	}
}