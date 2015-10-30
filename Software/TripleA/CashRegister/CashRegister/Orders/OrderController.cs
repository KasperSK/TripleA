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
        public List<SalesOrder> StashedOrders { get; }
        public SalesOrder CurrentOrder { get; private set; }

	    public OrderController (IOrderDao orderDao)
	    {
	        OrderDao = orderDao;
            StashedOrders = new List<SalesOrder>();
	    }

        public virtual void CreateNewOrder()
        {
            StashCurrentOrder();
            CurrentOrder = new SalesOrder() { Lines = new List<OrderLine>(), Transactions = new List<Transaction>() };
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

            CurrentOrder = StashedOrders[id];
            StashedOrders.RemoveAt(id);
        }

        private void StashCurrentOrder()
        {
            if (CurrentOrder != null)
                StashedOrders.Add(CurrentOrder);

            CurrentOrder = null;
        }

        public void ClearOrder()
        {
            if (CurrentOrder == null)
                return;

            CurrentOrder.Lines.Clear();
            CurrentOrder.Total=0;
        }

        public void AddProduct(Product product, int quantity = 1, Discount discount = null)
        {
            if (CurrentOrder == null)
                return;

            var orderLine = new OrderLine()
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

        public IEnumerable<SalesOrder> GetNLastOrders(int n)
        {
            var orders = OrderDao.GetNLastOrders(n);
            return orders;
        }
	}
}