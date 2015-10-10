using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.CashRegister.DAL
{
    interface IUnitOfWork
    {
        void Save();
    }
}
