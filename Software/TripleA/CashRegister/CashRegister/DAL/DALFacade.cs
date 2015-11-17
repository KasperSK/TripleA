using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Database;
using CashRegister.DAL;

namespace CashRegister.DAL
{
    public class DalFacade : IDalFacade
    {
        private CashRegisterContext _context;
        private UnitOfWork _unitOfWork;
        private bool _disposed = false;

        public IUnitOfWork GetUnitOfWork()
        {
            if(_unitOfWork != null)
                throw new Exception("The unit of work is in use");

            _context = new CashRegisterContext();
            _unitOfWork = new UnitOfWork(_context, this);
            return _unitOfWork;
        }

        public void ReturnUnitOfWork()
        {
            _unitOfWork = null;
            _context = null;
        }

        public string DatabaseName { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
