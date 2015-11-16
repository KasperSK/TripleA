using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Models;

namespace CashRegister.CashRegister.Payment
{
    public interface IPaymentDao
    {
        void Delete(PaymentType paymentType);
        void Delete(Transaction transaction);

        void Update(PaymentType paymentType);
        void Update(Transaction transaction);

        void Insert(PaymentType paymentType);
        void Insert(Transaction transaction);

        PaymentType SelectByPaymentTypeId(long id);
        Transaction SelectByTransactionId(long id);

    }
}
