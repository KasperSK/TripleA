using System;
using CashRegister.Database;

namespace CashRegister.Dal
{
    public class DalFacade : IDalFacade
    {
        private CashRegisterContext _context;
        private bool _disposed;
        private UnitOfWork _unitOfWork;

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

        public string DatabaseName { get; set; }

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
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}