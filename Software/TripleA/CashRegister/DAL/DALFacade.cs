using System;
using System.Data.Common;
using CashRegister.Database;

namespace CashRegister.Dal
{
    public class DalFacade : IDalFacade
    {
        private CashRegisterContext _context;
        private bool _disposed;
        private IUnitOfWork _unitOfWork;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork != null)
                    throw new InvalidOperationException("The unit of work is in use");

                _context = DbConnection != null ? new CashRegisterContext(DbConnection, null) : new CashRegisterContext();
                _unitOfWork = new UnitOfWork(_context, this);
                return _unitOfWork;
            }
        }

        public DbConnection DbConnection { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void ReturnUnitOfWork()
        {
            _unitOfWork = null;
            _context = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
                _disposed = true;
            }
        }
    }
}