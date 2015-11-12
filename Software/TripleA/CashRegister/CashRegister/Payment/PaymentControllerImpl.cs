using System.Collections.Generic;
using System.Linq;
using CashRegister.CashDrawers;
using CashRegister.Printer;

namespace CashRegister.Payment
{
    public class PaymentControllerImpl : IPaymentController
    {
        public PaymentControllerImpl(List<PaymentProvider> PDList, IPrinter bonPrinter)
        {
            BonPrinter = bonPrinter;

            if (PDList == null)
            {
                PDList = new List<PaymentProvider> {new CashPayment()};
            }
            else
            {
                PaymentProviders = PDList;
            }
        }

        private List<PaymentProvider> PaymentProviders { get; }

        private CashDrawer cashDrawer { get; set; }

        public virtual IPrinter BonPrinter { get; set; }

        public IEnumerable<IPaymentProvidorDescriptor> PaymentProvidorDescriptors => PaymentProviders;

        public virtual bool ExecuteTransaction(ITransaction transaction, IPaymentProvidorDescriptor paymentProvidorDescriptor)
        {
            var paymentProvider = PaymentProviders.First(p => p.ID == paymentProvidorDescriptor.ID);

            var transferSuccess = paymentProvider.TransferAmount(transaction.Amount, transaction.Description);
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


        public void PrintTransaction(ITransaction transaction)
        {
            BonPrinter.AddTo("Betalt via: " + transaction.PaymentDescriptor.Description);
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