using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Models;

namespace CashRegister.Payment
{
    public interface IPaymentDao
    {
        
        void Delete(Transaction transaction);

        
        void Update(Transaction transaction);

        
        void Insert(Transaction transaction);

        
        Transaction SelectByTransactionId(long id);

    }
}
