using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using CashRegister.Dal;
using CashRegister.Log;
using CashRegister.Models;
using CashRegister.Orders;
using CashRegister.Payment;
using CashRegister.Printer;
using CashRegister.Products;
using CashRegister.Receipts;
using JetBrains.Annotations;

namespace CashRegister.Sales
{
    /// <summary>
    /// A Factory that builds a SalesController.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class SalesFactory
    {
        /// <summary>
        /// Contains a SalesController for the GUI.
        /// </summary>
        public static ISalesController GuiSalesController
        {
            get
            {
                var dalFacade = new DalFacade();
                var productController = new ProductController(new ProductDao(dalFacade));
                var receiptController = new ReceiptController(new ReceiptPrinter(), CultureInfo.InvariantCulture);
                var paymentController = Factory.GetPaymentController(receiptController, dalFacade, 0);
                var orderController = new OrderController(new OrderDao(dalFacade));
                return new SalesController(orderController, receiptController, productController, paymentController);
            }
        }
    }

    /// <summary>
    /// Implementation of the SalesController.
    /// Controls sales for the CashRegister.
    /// </summary>
    public class SalesController : ISalesController
    {
        /// <summary>
        /// Contains an implementation of IPaymentController.
        /// </summary>
        private readonly IPaymentController _paymentController;

        /// <summary>
        /// Contains an implementation of IProductController.
        /// </summary>
        private readonly IProductController _productController;

        /// <summary>
        /// Contains an implementation of IOrderController.
        /// </summary>
        private readonly IOrderController _orderController;

        /// <summary>
        /// Contains an implementation of IReceiptController.
        /// </summary>
        private readonly IReceiptController _receiptController;

        /// <summary>
        /// Contains an implementation of ILogger.
        /// </summary>
        private readonly ILogger _logger;

        ///<summary>
        /// Contains the current SalesOrder.
        /// </summary>
        public SalesOrder CurrentOrder => _orderController.CurrentOrder;

        /// <summary>
        /// Contains the Collection of OrderLines in the current SalesOrder.
        /// </summary>
        public IEnumerable<OrderLine> CurrentOrderLines => CurrentOrder.Lines;

        /// <summary>
        /// Contains the current total price of the SalesOrder.
        /// </summary>
        public int CurrentOrderTotal => CurrentOrder.Total;

        /// <summary>
        /// Contains the ProductTabs to GUI.
        /// </summary>
        public IReadOnlyCollection<ProductTab> ProductTabs => _productController.ProductTabs;

        /// <summary>
        /// Gets a list of all incomplete SalesOrder's by default current data (or within a certain date or time).
        /// </summary>
        public IReadOnlyCollection<SalesOrder> IncompleteOrders => _orderController.StashedOrders;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="orderController">An implementation of IOrderController.</param>
        /// <param name="receiptController">An implementation of IReceiptController.</param>
        /// <param name="productController">An implementation of IProductController.</param>
        /// <param name="paymentController">An implentation of IPaymenController.</param>
        public SalesController(IOrderController orderController, IReceiptController receiptController,
            IProductController productController, IPaymentController paymentController)
        {
            _orderController = orderController;
            _receiptController = receiptController;
            _productController = productController;
            _paymentController = paymentController;
            _logger = LogFactory.GetLogger(typeof (SalesController));
            _orderController.PropertyChanged += RepeatNotify;
        }

        /// <summary>
        /// Adds products to the current SalesOrder.
        /// </summary>
        /// <param name="product">The Product to be added to the SalesOrder.</param>
        /// <param name="quantity">The quantity of the Product. Can be negative.</param>
        /// <param name="discount">A discount on the product. Can be null.</param>
        public void AddProductToOrder(Product product, int quantity, Discount discount)
        {
            _orderController.AddProduct(product, quantity, discount);
            _logger.Debug("Product Added");
        }

        /// <summary>
        /// Tallies the sales for the CashRegister.
        /// </summary>
        /// <returns>The turnover for the sales done.</returns>
        public string Tally()
        {
            return _paymentController.Tally();
        }

        /// <summary>
        /// Prints a SalesOrder.
        /// </summary>
        public void CreateAndPrintReceipt()
        {
            _receiptController.CreateReceipt(_orderController.CurrentOrder);
        }

        /// <summary>
        /// Removes a Product from SalesOrder by added a OrderLine that has a negative quantity.
        /// </summary>
        /// <param name="product">The Product to be removed from the SalesOrder.</param>
        /// <param name="quantity">The quantity of the Product.</param>
        /// <param name="discount">A discount on the product. Can be null.</param>
        public void RemoveProductFromOrder(Product product, int quantity, Discount discount)
        {
            _orderController.AddProduct(product, -quantity, discount);
        }

        /// <summary>
        /// Removes OrderLines from the current SalesOrder.
        /// </summary>
        public void CancelOrder()
        {
            if (_orderController.CurrentOrder.Transactions.Count > 0)
            {
                _orderController.SaveOrder();
            }
            else
            {
                _orderController.ClearOrder();
            }
        }

        /// <summary>
        /// Saves a SalesOrder as incomplete.
        /// </summary>
        public void SaveIncompleteOrder()
        {
            _orderController.SaveOrder();
        }

        /// <summary>
        /// Starts a payment on the current SalesOrder.
        /// </summary>
        /// <param name="amountToPay">The amount to be payed.</param>
        /// <param name="description">A description for the payment.</param>
        /// <param name="provider">The provider for the payment.</param>
        public void StartPayment(int amountToPay, string description, PaymentType provider)
        {
            var descriptionAndSalesOrderId = description + " " + _orderController.CurrentOrder.Id;
            CreateTransaction(amountToPay, descriptionAndSalesOrderId, provider);
            if (MissingPaymentOnOrder() == 0)
            {
                _orderController.CurrentOrder.Status = OrderStatus.Completed;
                //CreateAndPrintReceipt();
                _orderController.SaveOrder();
            }
        }

        /// <summary>
        /// Calculates and returns the missing amount on the current SalesOrder before total is reached.
        /// </summary>
        /// <returns>The missing amount.</returns>
        public long MissingPaymentOnOrder()
        {
            return _orderController.MissingAmount();
        }

        /// <summary>
        /// Retrieve an incomplete SalesOrder.
        /// </summary>
        /// <param name="orderId">The id of the incomplete SalesOrder.</param>
        public void RetrieveIncompleteOrder(int orderId)
        {
            _orderController.GetStashedOrder(orderId);
        }

        /// <summary>
        /// Creates and returns a Transaction.
        /// </summary>
        /// <param name="amountToPay">The amount to be payed.</param>
        /// <param name="description">A description for the payment.</param>
        /// <param name="payment">The provider for the payment.</param>
        /// <returns>A Transaction.</returns>
        public Transaction CreateTransaction(int amountToPay, string description, PaymentType payment)
        {
            var transaction = new Transaction
            {
                Date = DateTime.Now,
                Status = TransactionStatus.Created,
                SalesOrder = _orderController.CurrentOrder,
                Price = amountToPay,
                PaymentType = payment,
                Description = description
            };

            var paymentCompleted = _paymentController.ExecuteTransaction(transaction);
            if (paymentCompleted)
            {
                transaction.Description = "Transaction completed";
                return transaction;
            }
            transaction.Description = "Transaction failed";

            return transaction;
        }

        /// <summary>
        /// Contains a collection of the PaymentProviderDescriptor's.
        /// </summary>
        public IEnumerable<IPaymentProviderDescriptor> PaymentProviderDescriptor => _paymentController.PaymentProviderDescriptors;

        private void RepeatNotify(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_orderController.CurrentOrder))
            {
                OnPropertyChanged(nameof(CurrentOrder));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}