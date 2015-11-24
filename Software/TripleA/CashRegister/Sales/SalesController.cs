using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using CashRegister.CashDrawers;
using CashRegister.Dal;
using CashRegister.Log;
using CashRegister.Models;
using CashRegister.Orders;
using CashRegister.Payment;
using CashRegister.Printer;
using CashRegister.Products;
using CashRegister.Receipts;

namespace CashRegister.Sales
{
    [ExcludeFromCodeCoverage]
    public static class SalesFactory
    {
        public static ISalesController GuiSalesController
        {
            get
            {
                var dalfacade = new DalFacade();
                var productController = new ProductController(new ProductDao(dalfacade));
                var receiptController = new ReceiptController(new ReceiptPrinter(), CultureInfo.InvariantCulture);
                var paymentController = Factory.GetPaymentController(receiptController, dalfacade, 0);
                var orderController = new OrderController(new OrderDao(dalfacade));
                return new SalesController(orderController, receiptController, productController, paymentController);
            }
        }
    }

    /// <summary>
    ///     Controls sales
    /// </summary>
    public class SalesController : ISalesController
    {
        private readonly IPaymentController _paymentController;
        private readonly IProductController _productController;
        private readonly IOrderController _orderController;
        private readonly IReceiptController _receiptController;
        private readonly ILogger _logger;


        /// <summary>
        ///     Contructor - Calls StartNewOrder()
        /// </summary>
        public SalesController(IOrderController orderController, IReceiptController receiptController,
            IProductController productController, IPaymentController paymentController)
        {
            _orderController = orderController;
            _receiptController = receiptController;
            _productController = productController;
            _paymentController = paymentController;
            _logger = LogFactory.GetLogger(typeof (SalesController));
        }

        /// <summary>
        ///     Add a product to an SalesOrder
        /// </summary>
        public void AddProductToOrder(Product product, int quantity, Discount discount)
        {
            _orderController.AddProduct(product, quantity, discount);
            OnPropertyChanged();
            _logger.Debug("Product Added");
        }

        /// <summary>
        ///     Prints an order
        /// </summary>
        public void CreateAndPrintReceipt()
        {
            _receiptController.CreateReceipt(_orderController.CurrentOrder);
            _orderController.SaveOrder();
        }


        /// <summary>
        ///     Remove a product from SalesOrder
        /// </summary>
        public void RemoveProductFromOrder(Product product, int quantity, Discount discount)
        {
            _orderController.AddProduct(product, -quantity, discount);
        }

        /// <summary>
        ///     Cancel transactions, clear SalesOrder
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
        ///     Save an Order as incomplete
        /// </summary>
        public void SaveIncompleteOrder()
        {
            _orderController.SaveOrder();
        }


        /// <summary>
        ///     Starting payment on a SalesOrder
        /// </summary>
        public void StartPayment(int amountToPay, string description, PaymentType provider)
        {
            var descriptionAndSalesOrderId = description + " " + _orderController.CurrentOrder.Id;
            CreateTransaction(amountToPay, descriptionAndSalesOrderId, provider);
            if (MissingPaymentOnOrder() == 0)
            {
                _orderController.SaveOrder();
                OnPropertyChanged("CurrentOrder");
            }
        }

        /// <summary>
        ///     Get info on the amount missing on the SalesOrder
        /// </summary>
        public long MissingPaymentOnOrder()
        {
            return _orderController.MissingAmount();
        }

        /// <summary>
        ///     Gets a list of all incomplete orders by default current data (or within a certain date or time)
        /// </summary>
        public IReadOnlyCollection<SalesOrder> IncompleteOrders => _orderController.StashedOrders;


        /// <summary>
        ///     Get an incomplete order
        /// </summary>
        public void RetrieveIncompleteOrder(int orderId)
        {
            _orderController.GetStashedOrder(orderId);
        }

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
            var paymentCompleted = true;
            paymentCompleted = _paymentController.ExecuteTransaction(transaction);
            if (paymentCompleted)
            {
                transaction.Description = "Transaction completed";
                return transaction;
            }
            transaction.Description = "Transaction failed";

            return transaction;
        }

        public SalesOrder CurrentOrder => _orderController.CurrentOrder;


        /// <summary>
        ///     Provides Product tabs to GUI
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<ProductTab> ProductTabs => _productController.ProductTabs;

        /// <summary>
        ///     Gets the PaymentProviderDescriptor
        /// </summary>
        public IEnumerable<IPaymentProviderDescriptor> PaymentProviderDescriptor => _paymentController.PaymentProviderDescriptors;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}