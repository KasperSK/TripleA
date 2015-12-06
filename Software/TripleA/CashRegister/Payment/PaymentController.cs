using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CashRegister.CashDrawers;
using CashRegister.Dal;
using CashRegister.Log;
using CashRegister.Models;
using CashRegister.Receipts;

namespace CashRegister.Payment
{
    /// <summary>
    /// Factory to create the payment controller
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Factory
    {
        /// <summary>
        /// Returns a paymentcontroller with all the nessesary payment providers
        /// </summary>
        /// <param name="receiptController">Receipt controller to be used by this payment controller</param>
        /// <param name="dalFacade">dalfacade to be used by this payment controller</param>
        /// <param name="startChange">The starting amount in the cashregister</param>
        /// <returns>The payment controller</returns>
        public static IPaymentController GetPaymentController(IReceiptController receiptController, IDalFacade dalFacade,
            int startChange)
        {
            var providers = new List<PaymentProvider>
            {
                new CashPayment(),
                new MobilePay(),
                new Nets(),
            };
            return new PaymentController(providers, receiptController, new PaymentDao(dalFacade), new CashDrawer(startChange));
        }
    }

    /// <summary>
    /// Implementation of payment controller
    /// </summary>
    public class PaymentController : IPaymentController
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="paymentProviderList">List of providers to be used with the payment controller</param>
        /// <param name="receiptController">The receipt controller to be used by the payment controller</param>
        /// <param name="paymentDao">The dao to be used by the payment controller</param>
        /// <param name="cashDrawer">The cashdrawer to be used by the payment controller</param>
        public PaymentController(IEnumerable<IPaymentProvider> paymentProviderList,
            IReceiptController receiptController, IPaymentDao paymentDao, ICashDrawer cashDrawer)
        {
            _receiptController = receiptController;
            _paymentDao = paymentDao;
            CashDrawer = cashDrawer;
            _paymentProviders = new List<IPaymentProvider>();
            foreach (var paymentProvider in paymentProviderList)
            {
                _paymentProviders.Add(paymentProvider);
            }
            _paymentProviders.ForEach(e => e.Init());
        }

        /// <summary>
        /// To log events
        /// </summary>
        private readonly ILogger _logger = LogFactory.GetLogger(typeof(PaymentController));

        /// <summary>
        /// Private list of providers
        /// </summary>
        private readonly List<IPaymentProvider> _paymentProviders;

        /// <summary>
        /// Private variable to hold the receiptcontroller
        /// </summary>
        private readonly IReceiptController _receiptController;

        /// <summary>
        /// Private variable to hold the dao object
        /// </summary>
        private readonly IPaymentDao _paymentDao;

        /// <summary>
        /// To hold the cashdrawer object
        /// </summary>
        private ICashDrawer CashDrawer { get; }

        /// <summary>
        /// Used to return the list of paayment provider descriptors
        /// </summary>
        public IEnumerable<IPaymentProviderDescriptor> PaymentProviderDescriptors => _paymentProviders;

        /// <summary>
        /// Is called to execute a transaction
        /// </summary>
        /// <param name="transaction">The transaction to execute</param>
        /// <returns>True if transaction went well</returns>
        public bool ExecuteTransaction(Transaction transaction)
        {
            var paymentProvider = _paymentProviders.First(p => p.Type == transaction.PaymentType);

            var transferSuccess = paymentProvider.TransferAmount(transaction.Price, transaction.Description);
            var transferStatus = paymentProvider.TransactionStatus();

            if (transferSuccess && transferStatus)
            {
                CashDrawer.Open();
                transaction.Status = TransactionStatus.Completed;
                _paymentDao.Insert(transaction);

                return true;
            }

            transaction.Status = TransactionStatus.Failed;
            _paymentDao.Insert(transaction);
            return false;
        }

        /// <summary>
        /// To print a transaction
        /// </summary>
        /// <param name="transaction">Transaction to be printet</param>
        public void PrintTransaction(Transaction transaction)
        {
            _receiptController.CreateReceipt(transaction);
        }

        /// <summary>
        /// To tally all the sales at the end of opening hours
        /// </summary>
        /// <returns>A string with all the sales in it</returns>
        public string Tally()
        {
            _logger.Debug("Reveneu");
            foreach (var paymentProvider in _paymentProviders)
            {
                _logger.Debug(paymentProvider.Name + ": " + paymentProvider.Revenue);
            }
            _logger.Debug("Total: " + _paymentProviders.Sum(p => p.Revenue));
            _logger.Debug("");
            _logger.Debug("Money in cashdrawer: " + (_paymentProviders.First(p => p.Type == PaymentType.Cash).Revenue +
                          CashDrawer.CashChange));

            string reply = "";
            reply += "Cash in drawer: " + (_paymentProviders.First(p => p.Type == PaymentType.Cash).Revenue +
                          CashDrawer.CashChange) + "";
            reply += "\nTotal: " + _paymentProviders.Sum(p => p.Revenue) + "\n";
            
            foreach (var paymentprovider in _paymentProviders)
            {
                reply += "\n" + paymentprovider.Name + ": " + paymentprovider.Tally();
            }

            return reply;
        }
    }
}