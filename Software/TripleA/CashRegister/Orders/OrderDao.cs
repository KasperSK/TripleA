using System.Collections.Generic;
using System.Linq;
using CashRegister.Dal;
using CashRegister.Models;

namespace CashRegister.Orders
{
    /// <summary>
    ///     Implementation of Data Access Objects for Orders
    /// </summary>
    public class OrderDao : IOrderDao
    {
        private readonly IDalFacade _dalFacade;

        public OrderDao(IDalFacade dalFacade)
        {
            _dalFacade = dalFacade;
        }

        /// <summary>
        ///     Delete an order in the database
        /// </summary>
        /// <param name="order">The order to be deleted</param>
        public virtual void Delete(SalesOrder order)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.SalesOrderRepository.Delete(order);
                uow.Save();
            }
        }

        /// <summary>
        ///     Update an order in the database
        /// </summary>
        /// <param name="order">The order to be updated</param>
        public virtual void Update(SalesOrder order)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.SalesOrderRepository.Update(order);
                uow.Save();
            }
        }

        /// <summary>
        ///     Insert an order in to the database
        /// </summary>
        /// <param name="order">The order to be inserted</param>
        public virtual void Insert(SalesOrder order)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.SalesOrderRepository.Insert(order);
                uow.Save();
            }
        }

        /// <summary>
        ///     Get an order from id
        /// </summary>
        /// <param name="id">The id of the order</param>
        /// <returns>An SalesOrder from an id</returns>
        public virtual SalesOrder SelectById(long id)
        {
            SalesOrder salesOrder;
            using (var uow = _dalFacade.UnitOfWork)
            {
                salesOrder = uow.SalesOrderRepository.GetById(id);
            }

            return salesOrder;
        }

        public virtual void AddOrderLine(OrderLine line)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.SalesOrderRepository.Update(line.SalesOrder);
                uow.OrderLineRepository.Insert(line);
                uow.Save();
            }
        }

        public void ClearOrder(SalesOrder order)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.SalesOrderRepository.Update(order);
                foreach (var orderLine in order.Lines.ToList())
                {
                    order.Lines.Remove(orderLine);
                    uow.OrderLineRepository.Delete(orderLine);
                }
                uow.Save();
            }
        }

        /// <summary>
        ///     Get a list of the last n orders
        /// </summary>
        /// <param name="amount">The amount of orders to be returned</param>
        /// <returns>A IEnumerable list of the last n orders</returns>
        public virtual IEnumerable<SalesOrder> GetLastOrders(int amount)
        {
            IEnumerable<SalesOrder> salesOrders;

            using (var uow = _dalFacade.UnitOfWork)
            {
                salesOrders = uow.SalesOrderRepository.Get(null, q => q.OrderBy(x => x.Id)).Take(amount);
            }

            return salesOrders;
        }

        /// <summary>
        ///     Get the last order
        /// </summary>
        /// <returns>The last SalesOrder</returns>
        public SalesOrder LastOrder
        {
            get
            {
                SalesOrder salesOrder;

                using (var uow = _dalFacade.UnitOfWork)
                {
                    salesOrder = uow.SalesOrderRepository.Get(null, q => q.OrderBy(x => x.Id)).Last();
                }

                return salesOrder;
            }
        }

        /// <summary>
        ///     Get the id of the last order
        /// </summary>
        /// <returns>The id of the last order</returns>
        public virtual long LastId
        {
            get
            {
                long id;

                using (var uow = _dalFacade.UnitOfWork)
                {
                    id = uow.SalesOrderRepository.Get(null, q => q.OrderBy(x => x.Id)).Last().Id;
                }

                return id;
            }
        }
    }
}