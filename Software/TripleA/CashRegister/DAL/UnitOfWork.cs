using System;
using CashRegister.Database;
using CashRegister.Models;

namespace CashRegister.Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CashRegisterContext _context;
        private readonly IDalFacade _controller;
        private IRepository<Discount> _discountRepository;

        private bool _disposed;
        private IRepository<OrderLine> _orderLineRepository;
        private IRepository<ProductGroup> _productGroupRepository;
        private IRepository<Product> _productRepository;
        private IRepository<ProductTab> _productTabRepository;
        private IRepository<ProductType> _productTypeRepository;
        private IRepository<SalesOrder> _salesOrderRepository;
        private IRepository<Transaction> _transactionRepository;

        public UnitOfWork(CashRegisterContext context, IDalFacade controller)
        {
            _context = context;
            _controller = controller;
        }

        public IRepository<Discount> DiscountRepository
            => _discountRepository ?? (_discountRepository = new Repository<Discount>(_context));

        public IRepository<OrderLine> OrderLineRepository
            => _orderLineRepository ?? (_orderLineRepository = new Repository<OrderLine>(_context));

        public IRepository<Product> ProductRepository
            => _productRepository ?? (_productRepository = new Repository<Product>(_context));

        public IRepository<ProductGroup> ProductGroupRepository
            => _productGroupRepository ?? (_productGroupRepository = new Repository<ProductGroup>(_context));

        public IRepository<ProductType> ProductTypeRepository
            => _productTypeRepository ?? (_productTypeRepository = new Repository<ProductType>(_context));

        public IRepository<ProductTab> ProductTabRepository
            => _productTabRepository ?? (_productTabRepository = new Repository<ProductTab>(_context));

        public IRepository<SalesOrder> SalesOrderRepository
            => _salesOrderRepository ?? (_salesOrderRepository = new Repository<SalesOrder>(_context));

        public IRepository<Transaction> TransactionRepository
            => _transactionRepository ?? (_transactionRepository = new Repository<Transaction>(_context));

        public void Save()
        {
            _context.SaveChanges();
        }

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
                    _context.Dispose();
                    _controller.ReturnUnitOfWork();
                }
            }
            _disposed = true;
        }
    }
}