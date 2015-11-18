using System;

namespace CashRegister.Dal
{
    public interface IDalFacade : IDisposable
    {
        string DatabaseName { get; set; }
        IUnitOfWork UnitOfWork { get; }
    }
}