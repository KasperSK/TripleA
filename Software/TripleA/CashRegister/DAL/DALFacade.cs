using System;
using System.Data.Common;
using CashRegister.Database;

namespace CashRegister.Dal
{
    /// <summary>
    /// This is the front of the data base it provides the unit of work and creates the database context
    /// </summary>
    public class DalFacade : IDalFacade
    {
        /// <summary>
        /// The database context
        /// </summary>
        private CashRegisterContext _context;

        /// <summary>
        /// To check if we are disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// the unitofwork being kept track of
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Returns the unit of work with a database context if the database is in use it throws an exception
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork != null)
                    throw new InvalidOperationException("The unit of work is in use");

                _context = new CashRegisterContext();
                _unitOfWork = new UnitOfWork(_context, this);
                return _unitOfWork;
            }
        }

        /// <summary>
        /// To dispose of the facade and the database
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Callback for when the unitofwork diposes
        /// </summary>
        public void ReturnUnitOfWork()
        {
            _unitOfWork = null;
            _context = null;
        }

        /// <summary>
        /// Internal dispose calls dispose on context and unitofwork
        /// </summary>
        /// <param name="disposing">True if diposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _unitOfWork.Dispose();
                }
                _disposed = true;
            }
        }
    }
}