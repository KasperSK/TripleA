using System.Collections.Generic;
using System.Linq;
using CashRegister.Dal;
using CashRegister.Models;

namespace CashRegister.Orders
{
    /// <summary>
    /// Implementation of SalesOrder DataAccessObjects.
    /// This controls how we access the SalesOrder and OrderLines in the database.
    /// </summary>
    public class OrderDao : IOrderDao
    {
        /// <summary>
        /// An interface implementation for the Data Access Logic Facade.
        /// </summary>
        private readonly IDalFacade _dalFacade;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dalFacade">A IDalFacade implementation.</param>
        public OrderDao(IDalFacade dalFacade)
        {
            _dalFacade = dalFacade;
        }

        /// <summary>
        /// Delete an SalesOrder in the database.
        /// </summary>
        /// <param name="order">The SalesOrder to be deleted.</param>
        public virtual void Delete(SalesOrder order)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.SalesOrderRepository.Delete(order);
                uow.Save();
            }
        }

        /// <summary>
        /// Update a SalesOrder in the database.
        /// </summary>
        /// <param name="order">The SalesOrder to be updated.</param>
        public virtual void Update(SalesOrder order)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.SalesOrderRepository.Update(order);
                uow.Save();
            }
        }

        /// <summary>
        /// Insert a SalesOrder in to the database.
        /// </summary>
        /// <param name="order">The SalesOrder to be inserted.</param>
        public virtual void Insert(SalesOrder order)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.SalesOrderRepository.Insert(order);
                uow.Save();
            }
        }

        /// <summary>
        /// Gets a SalesOrder from id.
        /// </summary>
        /// <param name="id">The id of the SalesOrder.</param>
        /// <returns>A SalesOrder from an id.</returns>
        public virtual SalesOrder SelectById(long id)
        {
            SalesOrder salesOrder;
            using (var uow = _dalFacade.UnitOfWork)
            {
                salesOrder = uow.SalesOrderRepository.GetById(id);
            }

            return salesOrder;
        }
        
        /// <summary>
        /// Inserts an OrderLine into the database.
        /// </summary>
        /// <param name="line">The OrderLine to be inserted</param>
        public virtual void AddOrderLine(OrderLine line)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.SalesOrderRepository.Update(line.SalesOrder);
                uow.OrderLineRepository.Insert(line);
                uow.Save();
            }
        }

        /// <summary>
        /// Clears the orderlines from an excisting SalesOrder.
        /// </summary>
        /// <param name="order">The SalesOrder to be cleared.</param>
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
        /// Get a list of the last n SalesOrder's.
        /// </summary>
        /// <param name="amount">The amount of SalesOrder's to be returned.</param>
        /// <returns>A IEnumerable list of the last n SalesOrder's.</returns>
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
        /// Get the last SalesOrder.
        /// </summary>
        /// <returns>Returns the last SalesOrder.</returns>
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
        /// Get the id of the last SalesOrder.
        /// </summary>
        /// <returns>The id of the last SalesOrder.</returns>
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