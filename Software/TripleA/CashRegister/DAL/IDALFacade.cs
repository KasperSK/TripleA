using System;
using System.Data.Common;

namespace CashRegister.Dal
{
    public interface IDalFacade : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        void ReturnUnitOfWork();
    }
}