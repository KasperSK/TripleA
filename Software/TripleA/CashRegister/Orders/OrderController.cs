using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CashRegister.Models;
using JetBrains.Annotations;

namespace CashRegister.Orders
{
    /// <summary>
    /// Implementation of IOrderController.
    /// This controller handles SalesOrders for the CashRegister.
    /// </summary>
	public class OrderController : IOrderController
	{
        /// <summary>
        /// An interface to the a SalesOrder Data Access Implementation.
        /// </summary>
        private IOrderDao OrderDao { get; }

        /// <summary>
        /// A collection of the stashed SalesOrders.
        /// </summary>
        private readonly List<SalesOrder> _stashedOrders;

        /// <summary>
        /// A read-only collection of the stashed SalesOrders.
        /// </summary>
        public IReadOnlyCollection<SalesOrder> StashedOrders => _stashedOrders;
        
        /// <summary>
        ///  The SalesOrder that is worked on by a instance of the class.
        /// </summary>
        public SalesOrder CurrentOrder { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="orderDao">An IOrderDao implementation.</param>
	    public OrderController(IOrderDao orderDao)
	    {
	        OrderDao = orderDao;
            CreateNewOrder();
            _stashedOrders = new List<SalesOrder>();
	    }

        /// <summary>
        /// Creates a new SalesOrder and inserts it into the database.
        /// </summary>
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

        /// <summary>
        /// Saves the current order.
        /// </summary>
        public void SaveOrder()
	    {
            CurrentOrder.Date = DateTime.Now;
            OrderDao.Update(CurrentOrder);
            
            CreateNewOrder();
        }

        /// <summary>
        /// Retrieves a stashed SalesOrder.
        /// </summary>
        /// <param name="id">The internal id of the stashed SalesOrder</param>
        public void GetStashedOrder(int id)
        {
            if (id > StashedOrders.Count)
                return;

            _stashedOrders.Add(CurrentOrder);
            CurrentOrder = _stashedOrders[id];
            _stashedOrders.RemoveAt(id);
            OnPropertyChanged(nameof(CurrentOrder));
        }

        /// <summary>
        /// Stashes the current SalesOrder.
        /// </summary>
        public void StashCurrentOrder()
        {
            _stashedOrders.Add(CurrentOrder);
            CreateNewOrder();
        }

        /// <summary>
        /// Clears the OrderLines from the current SalesOrder.
        /// </summary>
        public void ClearOrder()
        {

            CurrentOrder.Date = DateTime.Now;
            OrderDao.ClearOrder(CurrentOrder);
            OnPropertyChanged(nameof(CurrentOrder));

        }

        /// <summary>
        /// Adds an amount of products to the current SalesOrder.
        /// </summary>
        /// <param name="product">Product to be added to the SalesOrder.</param>
        public void AddProduct(Product product)
        {
            AddProduct(product, 1);
        }

        /// <summary>
        /// Adds an amount of products to the current order.
        /// </summary>
        /// <param name="product">Product to be added to the SalesOrder.</param>
        /// <param name="quantity">The quantity of the Product. Can be negative.</param>
        public void AddProduct(Product product, int quantity)
        {
            AddProduct(product, quantity, null);
        }

        /// <summary>
        /// Adds an amount of products to the current order.
        /// </summary>
        /// <param name="product">Product to be added to the SalesOrder.</param>
        /// <param name="quantity">The quantity of the product. Can be negative.</param>
        /// <param name="discount">The discount on the product. Can be null.</param>
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
        
        /// <summary>
        /// Get the missing amount from the current SalesOrder.
        /// </summary>
        /// <returns>The missing amount on the current SalesOrder.</returns>
        public long MissingAmount()
        {
            return (CurrentOrder != null ? CurrentOrder.Total - CurrentOrder.Transactions.Sum(t => t.Price) : 0);
        }

        /// <summary>
        /// Get a SalesOrder by id and set it as the CurrentOrder.
        /// </summary>
        /// <param name="id">The wanted SalesOrder id.</param>
        public void GetOrderById(long id)
        {
            StashCurrentOrder();
            CurrentOrder = OrderDao.SelectById(id);
        }

        /// <summary>
        /// Gets the last n SalesOrder.
        /// </summary>
        /// <param name="amount">The amount of orders to be returned.</param>
        public IEnumerable<SalesOrder> GetLastOrders(int amount)
        {
            var orders = OrderDao.GetLastOrders(amount);
            return orders;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
	}
}