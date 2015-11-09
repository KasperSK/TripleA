using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.DAL
{
    public interface IDalFacade
    {
        IUnitOfWork GetUnitOfWork();

        string DatabaseName { get; set; }
    }
}
