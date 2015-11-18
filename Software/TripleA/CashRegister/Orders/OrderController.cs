using System;
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
        private readonly List<SalesOrder> _stashedOrders; 
        public IReadOnlyCollection<SalesOrder> StashedOrders => _stashedOrders;
        public SalesOrder CurrentOrder { get; private set; }

	    public OrderController (IOrderDao orderDao)
	    {
	        OrderDao = orderDao;
            _stashedOrders = new List<SalesOrder>();
	    }

        public void CreateNewOrder()
        {
            StashCurrentOrder();
            CurrentOrder = new SalesOrder {Status = OrderStatus.Created };
        }

        public void SaveOrder()
	    {
            if (CurrentOrder == null)
                return;

            CurrentOrder.Date = DateTime.Now;
            OrderDao.Insert(CurrentOrder);
            
            CurrentOrder = null;
	    }

        public void UpdateOrder()
        {
            if (CurrentOrder == null)
                return;

            OrderDao.Update(CurrentOrder);

            CurrentOrder = null;
        }

        public void GetStashedOrder(int id)
        {
            if (id > StashedOrders.Count)
                return;

            StashCurrentOrder();

            CurrentOrder = _stashedOrders[id];
            _stashedOrders.RemoveAt(id);
        }

        private void StashCurrentOrder()
        {
            if (CurrentOrder != null)
                _stashedOrders.Add(CurrentOrder);

            CurrentOrder = null;
        }

        public void ClearOrder()
        {
            if (CurrentOrder == null)
                return;

            CurrentOrder.Lines.Clear();
            CurrentOrder.Total=0;
        }

        // Fixes: Default parameters should not be used
        public void AddProduct(Product product)
        {
            AddProduct(product, 1);
        }

        // Fixes: Default parameters should not be used
        public void AddProduct(Product product, int quantity)
        {
            AddProduct(product, quantity, null);
        }

        public void AddProduct(Product product, int quantity, Discount discount)
        {
            if (CurrentOrder == null)
                return;

            var orderLine = new OrderLine
            {
                Product = product,
                Quantity = quantity,
                Discount = discount,
                UnitPrice = product.Price,
                DiscountValue = (discount == null ? 0 : discount.Percent / 100 * product.Price)
            };

            CurrentOrder.Lines.Add(orderLine);
            CurrentOrder.Total += (orderLine.UnitPrice - orderLine.DiscountValue) * orderLine.Quantity;
        }

        public void AddTransaction(Transaction transaction)
        {
            if (CurrentOrder == null)
                return;

            CurrentOrder.Transactions.Add(transaction);
        }

        public long MissingAmount()
        {
            return (CurrentOrder != null ? CurrentOrder.Total - CurrentOrder.Transactions.Sum(t => t.Price) : 0);
        }

        public void GetOrderById(long id)
        {
            StashCurrentOrder();
            CurrentOrder = OrderDao.SelectById(id);
        }

        public IEnumerable<SalesOrder> GetLastOrders(int amount)
        {
            var orders = OrderDao.GetLastOrders(amount);
            return orders;
        }
	}
}