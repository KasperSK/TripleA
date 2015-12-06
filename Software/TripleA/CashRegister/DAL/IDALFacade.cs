using System;
using System.Data.Common;

namespace CashRegister.Dal
{
    /// <summary>
    /// Interface for the facade
    /// </summary>
    public interface IDalFacade : IDisposable
    {
        /// <summary>
        /// Must be able to get unit of work
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// To return the unit of work
        /// </summary>
        void ReturnUnitOfWork();
    }
}