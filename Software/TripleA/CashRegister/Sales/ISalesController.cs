using System.Collections.Generic;
using System.ComponentModel;
using CashRegister.Models;
using CashRegister.Payment;

namespace CashRegister.Sales
{
    /// <summary>
    /// An Interface for the SalesController.
    /// Controls sales for the CashRegister.
    /// </summary>
    public interface ISalesController : INotifyPropertyChanged
    {
        ///<summary>
        /// Contains the current SalesOrder.
        /// </summary>
        SalesOrder CurrentOrder { get; }

        /// <summary>
        /// Contains the Collection of OrderLines in the current SalesOrder.
        /// </summary>
        IEnumerable<OrderLine> CurrentOrderLines { get; }

        /// <summary>
        /// Contains the current total price of the SalesOrder.
        /// </summary>
        int CurrentOrderTotal { get; }

        /// <summary>
        /// Contains a collection of the PaymentProviderDescriptor's.
        /// </summary>
        IEnumerable<IPaymentProviderDescriptor> PaymentProviderDescriptor { get; }

        /// <summary>
        /// Contains the ProductTabs to GUI.
        /// </summary>
        IReadOnlyCollection<ProductTab> ProductTabs { get; }

        /// <summary>
        /// Gets a list of all incomplete SalesOrder's by default current data (or within a certain date or time).
        /// </summary>
        IReadOnlyCollection<SalesOrder> IncompleteOrders { get; }

        /// <summary>
        /// Adds products to the current SalesOrder.
        /// </summary>
        /// <param name="product">The Product to be added to the SalesOrder.</param>
        /// <param name="quantity">The quantity of the Product. Can be negative.</param>
        /// <param name="discount">A discount on the product. Can be null.</param>
        void AddProductToOrder(Product product, int quantity, Discount discount);

        /// <summary>
        /// Tallies the sales for the CashRegister.
        /// </summary>
        /// <returns>The turnover for the sales done.</returns>
        string Tally();

        /// <summary>
        /// Removes a Product from SalesOrder by added a OrderLine that has a negative quantity.
        /// </summary>
        /// <param name="product">The Product to be removed from the SalesOrder.</param>
        /// <param name="quantity">The quantity of the Product.</param>
        /// <param name="discount">A discount on the product. Can be null.</param>
        void RemoveProductFromOrder(Product product, int quantity, Discount discount);
        
        /// <summary>
        /// Prints a SalesOrder.
        /// </summary>
        void CreateAndPrintReceipt();

        /// <summary>
        /// Removes OrderLines from the current SalesOrder.
        /// </summary>
        void CancelOrder();

        /// <summary>
        /// Saves a SalesOrder as incomplete.
        /// </summary>
        void SaveIncompleteOrder();

        /// <summary>
        /// Starts a payment on the current SalesOrder.
        /// </summary>
        /// <param name="amountToPay">The amount to be payed.</param>
        /// <param name="description">A description for the payment.</param>
        /// <param name="provider">The provider for the payment.</param>
        void StartPayment(int amountToPay, string description, PaymentType provider);

        /// <summary>
        /// Calculates and returns the missing amount on the current SalesOrder before total is reached.
        /// </summary>
        /// <returns>The missing amount.</returns>
        long MissingPaymentOnOrder();

        /// <summary>
        /// Retrieve an incomplete SalesOrder.
        /// </summary>
        /// <param name="orderId">The id of the incomplete SalesOrder.</param>
        void RetrieveIncompleteOrder(int orderId);

        /// <summary>
        /// Creates and returns a Transaction.
        /// </summary>
        /// <param name="amountToPay">The amount to be payed.</param>
        /// <param name="description">A description for the payment.</param>
        /// <param name="payment">The provider for the payment.</param>
        /// <returns>A Transaction.</returns>
        Transaction CreateTransaction(int amountToPay, string description, PaymentType payment);
    }
}

