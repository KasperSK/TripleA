using CashRegister.Models;

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
        public SalesController(List<IPaymentProvidorDescriptor> paymentProvidorDescriptors)
        {
            PaymentProvidorDescriptors = paymentProvidorDescriptors;
        }

        /// <summary>
        /// Holds The payment providers
        /// </summary>
        private List<IPaymentProvidorDescriptor> PaymentProvidorDescriptors { get; set; }


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
       /* public List<IPaymentProvidorDescriptor> GetPaymentProviderDescriptor()
         {
             var PaymenProvidors = new PaymenControllerImpl;
             PaymentProvidorDescriptors = PaymenProvidors.PaymentProvidorDescriptors;
             return PaymentProvidorDescriptors;
         }*/
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
            OrderController.AddProduct(product, 1, null);
        }

        /// <summary>
        /// Prints an order
        /// </summary>
        public void CreateAndPrintReceipt(SalesOrder order)
        {
            var print = ReceiptController.CreateReceipt(order);
            ReceiptController.Print(print);
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
            OrderController.MissingAmount();
            if (OrderController.MissingAmount() == 0)
                StartNewOrder();
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
        public void StartPayment(IPaymentProvidorDescriptor provider, int amountToPay)
        {

            CreateTransaction(amountToPay, "Bla", provider);
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

        public Transaction CreateTransaction(int amountToPay, string description, IPaymentProvidorDescriptor payment)
        {
            var trans = new Transaction();
            trans.Amount = amountToPay;
            trans.Description = description;
            //trans.PaymentDesciptor = payment;
            return trans;
        }

    }
}

