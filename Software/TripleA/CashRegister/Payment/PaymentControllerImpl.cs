using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Controls;
using CashRegister.CashDrawers;
using CashRegister.Payment;
using CashRegister.Models;
using CashRegister.Printer;
using CashRegister.Receipts;

namespace CashRegister.Payment
{
    public class PaymentControllerImpl : IPaymentController
    {
        public PaymentControllerImpl(IReadOnlyCollection<PaymentProvider> paymentProviderList, IReceiptController receiptController, IPaymentDao paymentDao, ICashDrawer cashDrawer)
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

        private readonly IPaymentDao _paymentDao;

        private IReadOnlyCollection<PaymentProvider> PaymentProviders { get; }

        private ICashDrawer _cashDrawer { get; set; }

        public IReceiptController ReceiptController { get; set; }

        public IEnumerable<IPaymentProvidorDescriptor> PaymentProvidorDescriptors => PaymentProviders;

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
            else
            {
                transaction.Status = TransactionStatus.Failed;
                _paymentDao.Insert(transaction);
                return false;
            }
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
                    startChange = paymentProvider._StartChange;
                }
                total += paymentProvider.Tally();
            }

            return total + startChange;
        }

        
    }
}