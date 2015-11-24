using System.Collections.Generic;
using System.Linq;
using CashRegister.CashDrawers;
using CashRegister.Dal;
using CashRegister.Log;
using CashRegister.Models;
using CashRegister.Receipts;

namespace CashRegister.Payment
{
    public static class Factory
    {
        public static IPaymentController GetPaymentController(IReceiptController receiptController, IDalFacade dalFacade,
            int startchange)
        {
            var providers = new List<PaymentProvider>
            {
                new CashPayment(),
                new MobilePay(),
                new Nets(),
            };
            return new PaymentController(providers, receiptController, new PaymentDao(dalFacade), new CashDrawer(startchange));
        }
    }

    public class PaymentController : IPaymentController
    {
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

        private readonly ILogger _logger = LogFactory.GetLogger(typeof(PaymentController));

        private readonly List<IPaymentProvider> _paymentProviders;
        private readonly IReceiptController _receiptController;
        private readonly IPaymentDao _paymentDao;
        private ICashDrawer CashDrawer { get; }


        public IEnumerable<IPaymentProviderDescriptor> PaymentProviderDescriptors => _paymentProviders;

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

        public void PrintTransaction(Transaction transaction)
        {
            var receip = _receiptController.CreateReceipt(transaction);
            _receiptController.Print(receip);
            ;
        }

        public int Tally()
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
            return _paymentProviders.Sum(p => p.Tally()) + CashDrawer.CashChange;
        }
    }
}