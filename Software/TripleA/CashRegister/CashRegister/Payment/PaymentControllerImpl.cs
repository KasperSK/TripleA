using System.Collections.Generic;
using System.Linq;
using CashRegister.CashDrawers;
using CashRegister.Models;
using CashRegister.Printer;
using CashRegister.Receipts;

namespace CashRegister.Payment
{
    public class PaymentControllerImpl : IPaymentController
    {
        public PaymentControllerImpl(List<PaymentProvider> paymentProviderList, IReceiptController receiptcontroller)
        {
           ReceiptController = receiptcontroller;

            if (paymentProviderList != null)
            {
                PaymentProviders = paymentProviderList;
            }
            else
            {
                paymentProviderList = new List<PaymentProvider> {new CashPayment()};
            }
        }

        private List<PaymentProvider> PaymentProviders { get; }

        private CashDrawer cashDrawer { get; set; }

        public IReceiptController ReceiptController { get; set; }

        public IEnumerable<IPaymentProvidorDescriptor> PaymentProvidorDescriptors => PaymentProviders;

        public virtual bool ExecuteTransaction(Transaction transaction)
        {
            var paymentProvider = PaymentProviders.First(p => p.Type == transaction.PaymentType);

            var transferSuccess = paymentProvider.TransferAmount(transaction.Price, transaction.Description);
            var transferStatus = paymentProvider.TransactionStatus();

            if (transferSuccess && transferStatus)
            {
                cashDrawer.Open();
                transaction.Status = TransactionStatus.Completed;
                return true;
            }
            else
            {
                transaction.Status = TransactionStatus.Failed;
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