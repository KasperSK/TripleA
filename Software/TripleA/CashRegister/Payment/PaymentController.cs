using System.Collections.Generic;
using System.Linq;
using CashRegister.CashDrawers;
using CashRegister.Models;
using CashRegister.Receipts;

namespace CashRegister.Payment
{
    public class PaymentController : IPaymentController
    {
        public PaymentController(IReadOnlyCollection<PaymentProvider> paymentProviderList,
            IReceiptController receiptController, IPaymentDao paymentDao, ICashDrawer cashDrawer)
        {
            _receiptController = receiptController;
            _paymentDao = paymentDao;
            CashDrawer = cashDrawer;

            PaymentProviders = paymentProviderList ?? new List<PaymentProvider> {new CashPayment(0)};
        }

        private IReadOnlyCollection<PaymentProvider> PaymentProviders { get; }
        private readonly IReceiptController _receiptController;
        private readonly IPaymentDao _paymentDao;
        private ICashDrawer CashDrawer { get; }

       

        public IEnumerable<IPaymentProviderDescriptor> PaymentProviderDescriptors => PaymentProviders;

        public virtual bool ExecuteTransaction(Transaction transaction)
        {
            var paymentProvider = PaymentProviders.First(p => p.Type == transaction.PaymentType);

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