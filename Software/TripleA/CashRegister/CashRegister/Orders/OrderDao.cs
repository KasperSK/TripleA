using System;
using System.Collections.Generic;
using System.Linq;
using CashRegister.Database;
using CashRegister.DAL;
using CashRegister.Models;

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
        public virtual void Delete(SalesOrder order)
        {
            OrderUnitOfWork.SalesOrderRepository.Delete(order);
        }

        /// <summary>
        /// Update an order in the database
        /// </summary>
        /// <param name="order">The order to be updated</param>
        public virtual void Update(SalesOrder order)
        {
            OrderUnitOfWork.SalesOrderRepository.Update(order);
        }

        /// <summary>
        /// Insert an order in to the database
        /// </summary>
        /// <param name="order">The order to be inserted</param>
        public virtual void Insert(SalesOrder order)
        {
            OrderUnitOfWork.SalesOrderRepository.Insert(order);
        }

        /// <summary>
        /// Get an order from id
        /// </summary>
        /// <param name="id">The id of the order</param>
        /// <returns>An SalesOrder from an id</returns>
        public virtual SalesOrder SelectById(long id)
        {
            return OrderUnitOfWork.SalesOrderRepository.GetById(id);
        }

        /// <summary>
        /// Get a list of the last n orders
        /// </summary>
        /// <param name="n">The amount of orders to be returned</param>
        /// <returns>A IEnumerable list of the last n orders</returns>
        public virtual IEnumerable<SalesOrder> GetNLastOrders(int n)
        {     
            return OrderUnitOfWork.SalesOrderRepository.Get(null, q => q.OrderByDescending(x => x.Id)).Take(n);
        }

        /// <summary>
        /// Get the last order
        /// </summary>
        /// <returns>The last SalesOrder</returns>
        public SalesOrder GetLastOrder()
        {
            return OrderUnitOfWork.SalesOrderRepository.Get(null, q => q.OrderByDescending(x => x.Id)).Last();
        }

        /// <summary>
        /// Get the id of the last order
        /// </summary>
        /// <returns>The id of the last order</returns>
        public virtual long GetLastId()
        {
            return OrderUnitOfWork.SalesOrderRepository.Get(null, q => q.OrderByDescending(x => x.Id)).Last().Id;
        }
    }
}