using System.Collections.ObjectModel;
using System.ComponentModel;
using CashRegister.Models;
using CashRegister.Payment;
using CashRegister.Products;

namespace CashRegister.Sales
{
    using Orders;
    using Payment;
    using Receipts;
    using Database;
    using DAL;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Controls sales
    /// </summary>
    public class SalesController : ISalesController, INotifyPropertyChanged
    {
        private readonly PaymentControllerImpl _paymentControllerImpl;
        private readonly IProductController _productController;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, eventArgs);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public SalesController(PaymentControllerImpl payment,IProductController controller)
        {
            _paymentControllerImpl = payment;
            _productController = controller;
        }

        /// <summary>
        /// Holds The payment providers
        /// </summary>
        private IEnumerable<PaymentType> SalesPaymentProvidorDescriptors { get; set; }


        public IReceiptController ReceiptController
        {
            get;
            set;
        }

        public IOrderController OrderController
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the PaymentProviderDescriptor
        /// </summary>
       public IEnumerable<PaymentType> GetPaymentProviderDescriptor()
         {
             return SalesPaymentProvidorDescriptors;
       }
        /// <summary>
        /// Contructor - Calls StartNewOrder()
        /// </summary>
        public SalesController(IOrderController ordercontroller, IReceiptController receiptcontroller)
        {
            OrderController = ordercontroller;
            ReceiptController = receiptcontroller;
            StartNewOrder();
        }

        /// <summary>
        /// Add a product to an SalesOrder
        /// </summary>
        public void AddProductToOrder(Product product, int quantity, Discount discount)
        {
            OrderController.AddProduct(product, quantity, discount);
            OnPropertyChanged("Product Added");
        }

        /// <summary>
        /// Prints an order
        /// </summary>
        public void CreateAndPrintReceipt()
        {
            ReceiptController.CreateReceipt(OrderController.CurrentOrder);
            OrderController.SaveOrder();
        }


        /// <summary>
        /// Remove a product from SalesOrder
        /// </summary>
        public void RemoveProductFromOrder(Product product, int quantity, Discount discount)
        {
            var quantity_ = (-1 * quantity);
            OrderController.AddProduct(product, quantity_, discount);
        }

        /// <summary>
        /// clear SalesOrder
        /// </summary>
        public void ClearOrder()
        {
            OrderController.ClearOrder();
        }

        /// <summary>
        /// Starts a new SalesOrder with a new id
        /// </summary>
        public void StartNewOrder()
        {
            OrderController.CreateNewOrder();
        }

        /// <summary>
        /// Cancel transactions, clear SalesOrder
        /// </summary>
        public void CancelOrder()
        {
            OrderController.ClearOrder();
            if (OrderController.MissingAmount() == 0)
            {
                OrderController.SaveOrder();
                StartNewOrder();
            }
        }

        /// <summary>
        /// Save an Order as incomplete
        /// </summary>
        public void SaveIncompleteOrder()
        {
            OrderController.CreateNewOrder();
        }



        /// <summary>
        /// Starting payment on a SalesOrder
        /// </summary>
        public void StartPayment(int amountToPay, string description, PaymentType provider)
        {
            string descriptionAndSalesOrderId = description + " " + OrderController.CurrentOrder.Id;
            var trans = CreateTransaction(amountToPay, descriptionAndSalesOrderId, provider);
            OrderController.CurrentOrder.Transactions.Add(trans);
            if (MissingPaymenOnOrder() == 0)
            {
                OrderController.SaveOrder();
                StartNewOrder();
            }
        }

        /// <summary>
        /// Get info on the amount missing on the SalesOrder
        /// </summary>
        public long MissingPaymenOnOrder()
        {
            return OrderController.MissingAmount();
        }

        /// <summary>
        /// Adds an transaction to an order
        /// </summary>

        public void AddTransaction(Models.Transaction trans)
        {
            OrderController.AddTransaction(trans);
        }


        /// <summary>
        /// Gets a list of all incomplete orders by default current data (or within a certain date or time)
        /// </summary>
        public List<SalesOrder> GetIncompleteOrders()
        {
            return OrderController.StashedOrders;
        }

        /// <summary>
        /// Get an incomplete order
        /// </summary>
        public void RetrieveIncompleteOrder(int orderId)
        {
               OrderController.GetStashedOrder(orderId);
        }

        public Transaction CreateTransaction(int amountToPay, string description, PaymentType payment)
        {
            var transaction = new Transaction
            {
                Id = OrderController.CurrentOrder.Id,
                Price = amountToPay,
                PaymentType = payment,
                Description = description
            };
            bool paymentCompleted = _paymentControllerImpl.ExecuteTransaction(transaction);
            if (paymentCompleted == true)
            {
                return transaction;
            }
            else
            {
                transaction.Description = "Transaction failed";
                return transaction;
            }
            
        }

        public void TransactionComplete(Transaction transaction)
        {
                OrderController.MissingAmount();
        }

        public SalesOrder GetCurrentOrder()
        {
            return OrderController.CurrentOrder;
        }



        /// <summary>
        /// Provides Product tabs to GUI
        /// </summary>
        /// <returns></returns>
       public IReadOnlyCollection<ProductTab> ProductTabs()
        {
            return _productController.ProductTabs;
        }
    }
}

