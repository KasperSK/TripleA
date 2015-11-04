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
        private IDalFacade _dalFacade;


        public OrderDao(IDalFacade dalFacade)
        {
            _dalFacade = dalFacade;
        }

        /// <summary>
        /// Delete an order in the database
        /// </summary>
        /// <param name="order">The order to be deleted</param>
        public virtual void Delete(SalesOrder order)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                uow.SalesOrderRepository.Delete(order);
                uow.Save();
            }
        }

        /// <summary>
        /// Update an order in the database
        /// </summary>
        /// <param name="order">The order to be updated</param>
        public virtual void Update(SalesOrder order)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                uow.SalesOrderRepository.Update(order);
                uow.Save();
            }
        }

        /// <summary>
        /// Insert an order in to the database
        /// </summary>
        /// <param name="order">The order to be inserted</param>
        public virtual void Insert(SalesOrder order)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                uow.SalesOrderRepository.Insert(order);
                uow.Save();
            }
        }

        /// <summary>
        /// Get an order from id
        /// </summary>
        /// <param name="id">The id of the order</param>
        /// <returns>An SalesOrder from an id</returns>
        public virtual SalesOrder SelectById(long id)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                return uow.SalesOrderRepository.GetById(id);
            }
        }

        /// <summary>
        /// Get a list of the last n orders
        /// </summary>
        /// <param name="n">The amount of orders to be returned</param>
        /// <returns>A IEnumerable list of the last n orders</returns>
        public virtual IEnumerable<SalesOrder> GetNLastOrders(int n)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                return uow.SalesOrderRepository.Get(null, q => q.OrderByDescending(x => x.Id)).Take(n);
            }
        }

        /// <summary>
        /// Get the last order
        /// </summary>
        /// <returns>The last SalesOrder</returns>
        public SalesOrder GetLastOrder()
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                return uow.SalesOrderRepository.Get(null, q => q.OrderByDescending(x => x.Id)).Last();
            }
        }

        /// <summary>
        /// Get the id of the last order
        /// </summary>
        /// <returns>The id of the last order</returns>
        public virtual long GetLastId()
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                return uow.SalesOrderRepository.Get(null, q => q.OrderByDescending(x => x.Id)).Last().Id;
            }
        }
    }
}