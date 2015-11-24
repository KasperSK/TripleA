using System;
using System.Data.Common;

namespace CashRegister.Dal
{
    public interface IDalFacade : IDisposable
    {
        DbConnection DbConnection { get; set; }
        IUnitOfWork UnitOfWork { get; }
        void ReturnUnitOfWork();
    }
}