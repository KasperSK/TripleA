using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CashRegister.Models;
using JetBrains.Annotations;

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
            CreateNewOrder();
            _stashedOrders = new List<SalesOrder>();
	    }

        private void CreateNewOrder()
        {
            CurrentOrder = new SalesOrder
            {
                Status = OrderStatus.Created,
                Date = DateTime.Now,
            };
            OrderDao.Insert(CurrentOrder);
            OnPropertyChanged(nameof(CurrentOrder));
        }

        public void SaveOrder()
	    {
            CurrentOrder.Date = DateTime.Now;
            OrderDao.Update(CurrentOrder);
            
            CreateNewOrder();
        }

        public void GetStashedOrder(int id)
        {
            if (id > StashedOrders.Count)
                return;

            _stashedOrders.Add(CurrentOrder);
            CurrentOrder = _stashedOrders[id];
            _stashedOrders.RemoveAt(id);
            OnPropertyChanged(nameof(CurrentOrder));
        }

        public void StashCurrentOrder()
        {
            _stashedOrders.Add(CurrentOrder);
            CreateNewOrder();
        }

        public void ClearOrder()
        {

            CurrentOrder.Date = DateTime.Now;
            OrderDao.ClearOrder(CurrentOrder);
            OnPropertyChanged(nameof(CurrentOrder));

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
            var orderLine = new OrderLine
            {
                SalesOrder = CurrentOrder,
                Product = product,
                Quantity = quantity,
                Discount = discount,
                UnitPrice = product.Price,
                DiscountValue = (discount == null ? 0 : discount.Percent / 100 * product.Price)
            };

            OrderDao.AddOrderLine(orderLine);
            OnPropertyChanged(nameof(CurrentOrder));
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
	}
}