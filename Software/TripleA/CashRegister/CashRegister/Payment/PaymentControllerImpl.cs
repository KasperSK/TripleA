using System;
using System.Collections.Generic;
using CashRegister.CashDrawers;
using CashRegister.Printer;

namespace CashRegister.Payment
{
    public class PaymentControllerImpl : IPaymentController
    {

        private List<PaymentProvider> PaymentProviders { get; set; }
        private CashDrawer cashDrawer { get; set; }

        public virtual IPrinter BonPrinter { get; set; }

        public IEnumerable<IPaymentProvidorDescriptor> PaymentProvidorDescriptors
        {
            get { return PaymentProviders; }
        }

        public PaymentControllerImpl(List<PaymentProvider> PDList)
        {

            if (PDList == null)
            {
                PDList = new List<PaymentProvider> {new CashPayment()};
            }
            else
            {
                PaymentProviders = PDList;
            }
        }

        public virtual void ExecuteTransaction(ITransaction transaction, IPaymentProvidorDescriptor PD)
        {
            PaymentProvider PaymentP = null;

            foreach (var PP in PaymentProviders)
            {
                if (PD.ID == PP.ID)
                {
                    PaymentP = PP;
                }
            }

            bool TA = PaymentP.TransferAmount(transaction.Amount, transaction.Description);
            bool TS = PaymentP.TransactionStatus();

            if (TA && TS)
            {
                cashDrawer.Open();
                transaction.Status = TransactionStatus.Completed;
            }
            else
            {
                transaction.Status = TransactionStatus.Failed;
            }

        }


        public void PrintTransaction(ITransaction transaction)
        {
            throw new NotImplementedException();
        }

        public int Tally()
        {
            int startChange = 0;
            int total = 0;
            foreach (var PP in PaymentProviders)
            {
                if (PP.GetType() == typeof (CashPayment))
                {
                    startChange = PP._StartChange;
                }
                total += PP.Tally();
            }

            return total += startChange;
        }
    }
}