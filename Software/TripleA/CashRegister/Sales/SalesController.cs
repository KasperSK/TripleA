using System.Collections.Generic;
using System.ComponentModel;
using CashRegister.CashDrawers;
using CashRegister.Dal;
using CashRegister.Models;
using CashRegister.Orders;
using CashRegister.Payment;
using CashRegister.Printer;
using CashRegister.Products;
using CashRegister.Receipts;

namespace CashRegister.Sales
{
    public static class SalesFactory
    {
        public static ISalesController GuiSalesController
        {
            get
            {
                var dalfacade = new DalFacade();
                var productController = new ProductController(new ProductDao(dalfacade));
                var receiptController = new ReceiptController(new ReceiptPrinter());
                var paymentController = new PaymentController(new List<PaymentProvider>() {new CashPayment(0), new MobilePay(), new Nets()}, receiptController, new PaymentDao(dalfacade), new CashDrawer());
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



        /// <summary>
        ///     Contructor - Calls StartNewOrder()
        /// </summary>
        public SalesController(IOrderController orderController, IReceiptController receiptController, IProductController productController, IPaymentController paymentController)
        {
            _orderController = orderController;
            _receiptController = receiptController;
            _productController = productController;
            _paymentController = paymentController;
            StartNewOrder();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Add a product to an SalesOrder
        /// </summary>
        public void AddProductToOrder(Product product, int quantity, Discount discount)
        {
            _orderController.AddProduct(product, quantity, discount);
            OnPropertyChanged("Product Added");
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
        ///     clear SalesOrder
        /// </summary>
        public void ClearOrder()
        {
            _orderController.ClearOrder();
        }

        /// <summary>
        ///     Starts a new SalesOrder with a new id
        /// </summary>
        public void StartNewOrder()
        {
            _orderController.CreateNewOrder();
        }

        /// <summary>
        ///     Cancel transactions, clear SalesOrder
        /// </summary>
        public void CancelOrder()
        {
            _orderController.ClearOrder();
            if (_orderController.MissingAmount() == 0)
            {
                _orderController.SaveOrder();
                StartNewOrder();
            }
        }

        /// <summary>
        ///     Save an Order as incomplete
        /// </summary>
        public void SaveIncompleteOrder()
        {
            _orderController.CreateNewOrder();
        }


        /// <summary>
        ///     Starting payment on a SalesOrder
        /// </summary>
        public void StartPayment(int amountToPay, string description, PaymentType provider)
        {
            var descriptionAndSalesOrderId = description + " " + _orderController.CurrentOrder.Id;
            var trans = CreateTransaction(amountToPay, descriptionAndSalesOrderId, provider);
            _orderController.CurrentOrder.Transactions.Add(trans);
            if (MissingPaymentOnOrder() == 0)
            {
                _orderController.SaveOrder();
                StartNewOrder();
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
        ///     Adds an transaction to an order
        /// </summary>
        public void AddTransaction(Transaction trans)
        {
            _orderController.AddTransaction(trans);
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
                Id = _orderController.CurrentOrder.Id,
                Price = amountToPay,
                PaymentType = payment,
                Description = description
            };
            var paymentCompleted = _paymentController.ExecuteTransaction(transaction);
            if (paymentCompleted)
            {
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

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, eventArgs);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Gets the PaymentProviderDescriptor
        /// </summary>
        public IEnumerable<IPaymentProviderDescriptor> PaymentProviderDescriptor => _paymentController.PaymentProviderDescriptors;
        

        public void TransactionComplete(Transaction transaction)
        {
            // FIX What is wrong
            _orderController.MissingAmount();
        }
    }
}