using CashRegister.Models;

namespace CashRegister.Sales
{
    using Payment;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Controls sales
    /// </summary>
    public interface ISalesController
    {
        /// <summary>
        /// Adds a product to the current order
        /// </summary>
        void AddProductToOrder(Product product, int quantity, Discount discount);

        /// <summary>
        /// Remove a product from SalesOrder
        /// </summary>
        void RemoveProductFromOrder(Product product, int quantity, Discount discount);

        /// <summary>
        /// Prints an order
        /// </summary>
        void CreateAndPrintReceipt();

        /// <summary>
        /// clear SalesOrder
        /// </summary>
        void ClearOrder();

        /// <summary>
        /// Starts a new SalesOrder with a new id
        /// </summary>
        void StartNewOrder();

        /// <summary>
        /// Cancel transactions, clear SalesOrder
        /// </summary>
        void CancelOrder();

        /// <summary>
        /// Save an Order as incomplete
        /// </summary>
        void SaveIncompleteOrder();

        /// <summary>
        /// Starting payment on a SalesOrder
        /// </summary>
        void StartPayment(int amountToPay, string description, IPaymentProvidorDescriptor provider);

        /// <summary>
        /// Get info on the amount missing on the SalesOrder
        /// </summary>
        long MissingPaymenOnOrder();

        /// <summary>
        /// Add an transaction to the order
        /// </summary>
        void AddTransaction(Models.Transaction trans);

        /// <summary>
        /// Gets a list of all incomplete orders by default current data (or within a certain date or time)
        /// </summary>
        List<SalesOrder> GetIncompleteOrders();

        /// <summary>
        /// Get an incomplete order
        /// </summary>
        void RetrieveIncompleteOrder(int orderId);


        /// <summary>
        /// Creates and return a Transaction
        /// </summary>
        /// <param name="amountToPay"></param>
        /// <param name="description"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        ObservableTransaction CreateTransaction(int amountToPay, string description, IPaymentProvidorDescriptor payment);

        ///<summary>
        /// Returns the current order
        /// </summary>
        SalesOrder GetCurrentOrder();
    }
}

