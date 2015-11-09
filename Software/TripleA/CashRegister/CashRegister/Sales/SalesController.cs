using CashRegister.Models;
using CashRegister.Payment;

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
    public sealed class SalesController : ISalesController
    {
        public SalesController(PaymentControllerImpl payment)
        {
            SalesPaymentProvidorDescriptors=payment.PaymentProvidorDescriptors;
        }

        /// <summary>
        /// Holds The payment providers
        /// </summary>
        private IEnumerable<IPaymentProvidorDescriptor> SalesPaymentProvidorDescriptors { get; set; }


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
       public IEnumerable<IPaymentProvidorDescriptor> GetPaymentProviderDescriptor()
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
        public void StartPayment(int amountToPay, string description, IPaymentProvidorDescriptor provider)
        {

            string descriptionAndSalesOrderId = description + OrderController.CurrentOrder.Id;

            CreateTransaction(amountToPay, descriptionAndSalesOrderId, provider);
            if (amountToPay == 0)
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

        public ObservableTransaction CreateTransaction(int amountToPay, string description, IPaymentProvidorDescriptor payment)
        {
            var trans = new ObservableTransaction();
            var transaction = new Transaction();
            trans.Amount = amountToPay;
            trans.Description = description;
            trans.PaymentDescriptor = payment;
           // OrderController.CurrentOrder.Transactions.Add(trans);
            return trans;
        }

        public void TransactionComplete(ObservableTransaction transaction)
        {
                OrderController.MissingAmount();
        }

        public SalesOrder GetCurrentOrder()
        {
            return OrderController.CurrentOrder;
        }

    }
}

