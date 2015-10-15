using System;
using System.Collections.Generic;
using System.Linq;
using CashRegister.Database;
using CashRegister.DAL;

namespace CashRegister.Orders
{
    /// <summary>
    /// Implementation of Data Access Objects for Orders
    /// </summary>
    public class OrderDao : IOrderDao
    {
        private OrderUnitOfWork OrderUnitOfWork { get; }

        public OrderDao()
        {
            OrderUnitOfWork = new OrderUnitOfWork(new CashRegisterContext());    
        }

        /// <summary>
        /// Delete an order in the database
        /// </summary>
        /// <param name="order">The order to be deleted</param>
        public virtual void Delete(OrderList order)
        {
            OrderUnitOfWork.OrderListRepository.Delete(order);
        }

        /// <summary>
        /// Update an order in the database
        /// </summary>
        /// <param name="order">The order to be updated</param>
        public virtual void Update(OrderList order)
        {
            OrderUnitOfWork.OrderListRepository.Update(order);
        }

        /// <summary>
        /// Insert an order in to the database
        /// </summary>
        /// <param name="order">The order to be inserted</param>
        public virtual void Insert(OrderList order)
        {
            OrderUnitOfWork.OrderListRepository.Insert(order);
        }

        /// <summary>
        /// Get an order from id
        /// </summary>
        /// <param name="id">The id of the order</param>
        /// <returns>An OrderList from an id</returns>
        public virtual OrderList SelectById(long id)
        {
            return OrderUnitOfWork.OrderListRepository.GetById(id);
        }

        /// <summary>
        /// Get a list of the last n orders
        /// </summary>
        /// <param name="n">The amount of orders to be returned</param>
        /// <returns>A IEnumerable list of the last n orders</returns>
        public virtual IEnumerable<OrderList> GetNLastOrders(int n)
        {
            var orders = (from o in OrderUnitOfWork.OrderListRepository.Get()
                orderby o.OrderDate descending
                select o).Take(n);
        
            return orders;
        }

        /// <summary>
        /// Get the last order
        /// </summary>
        /// <returns>The last OrderList</returns>
        public OrderList GetLastOrder()
        {
            var order = (from o in OrderUnitOfWork.OrderListRepository.Get()
                orderby o.OrderId descending
                select o).Last();

            return order;
        }

        /// <summary>
        /// Get the id of the last order
        /// </summary>
        /// <returns>The id of the last order</returns>
        public virtual long GetLastId()
        {
            var id = (from o in OrderUnitOfWork.OrderListRepository.Get()
                orderby o.OrderId descending
                select o).Last();

            return id.OrderId;
        }
    }
}