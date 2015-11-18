using System.Collections.Generic;
using System.Linq;
using CashRegister.CashDrawers;
using CashRegister.Models;
using CashRegister.Receipts;

namespace CashRegister.Payment
{
    public class PaymentController : IPaymentController
    {
        private readonly IPaymentDao _paymentDao;

        public PaymentController(IReadOnlyCollection<PaymentProvider> paymentProviderList,
            IReceiptController receiptController, IPaymentDao paymentDao, ICashDrawer cashDrawer)
        {
            ReceiptController = receiptController;
            _paymentDao = paymentDao;

            _cashDrawer = cashDrawer;

            if (paymentProviderList != null)
            {
                PaymentProviders = paymentProviderList;
            }
            else
            {
                PaymentProviders = new List<PaymentProvider> {new CashPayment(0)};
            }
        }

        private IReadOnlyCollection<PaymentProvider> PaymentProviders { get; }

        private ICashDrawer _cashDrawer { get; }

        public IReceiptController ReceiptController { get; set; }

        public IEnumerable<IPaymentProviderDescriptor> PaymentProviderDescriptors => PaymentProviders;

        public virtual bool ExecuteTransaction(Transaction transaction)
        {
            var paymentProvider = PaymentProviders.First(p => p.Type == transaction.PaymentType);

            var transferSuccess = paymentProvider.TransferAmount(transaction.Price, transaction.Description);
            var transferStatus = paymentProvider.TransactionStatus();

            if (transferSuccess && transferStatus)
            {
                _cashDrawer.Open();
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
            var receip = ReceiptController.CreateReceipt(transaction);
            ReceiptController.Print(receip);
            ;
        }

        public int Tally()
        {
            var startChange = 0;
            var total = 0;
            foreach (var paymentProvider in PaymentProviders)
            {
                if (paymentProvider.GetType() == typeof (CashPayment))
                {
                    startChange = paymentProvider.StartChange;
                }
                total += paymentProvider.Tally();
            }

            return total + startChange;
        }
    }
}