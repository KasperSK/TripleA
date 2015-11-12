using System;
using CashRegister.Database;
using CashRegister.Models;

namespace CashRegister.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CashRegisterContext _context;
        private readonly DalFacade _controller;
        private IRepository<Discount> _discountRepository;
        private IRepository<OrderLine> _orderLineRepository;
        private IRepository<OrderStatus> _orderStatusRepository;
        private IRepository<PaymentType> _paymentTypeRepository;
        private IRepository<Product> _productRepository;
        private IRepository<ProductGroup> _productGroupRepository;
        private IRepository<ProductTab> _productTabRepository;
        private IRepository<SalesOrder> _salesOrderRepository;
        private IRepository<Transaction> _transactionRepository;


        public IRepository<Discount> DiscountRepository => _discountRepository ?? (_discountRepository = new Repository<Discount>(_context));

        public IRepository<OrderLine> OrderLineRepository
            => _orderLineRepository ?? (_orderLineRepository = new Repository<OrderLine>(_context));

        public IRepository<OrderStatus> OrderStatusRepository
            => _orderStatusRepository ?? (_orderStatusRepository = new Repository<OrderStatus>(_context));

        public IRepository<PaymentType> PaymentTypeRepository
            => _paymentTypeRepository ?? (_paymentTypeRepository = new Repository<PaymentType>(_context));

        public IRepository<Product> ProductRepository
            => _productRepository ?? (_productRepository = new Repository<Product>(_context));

        public IRepository<ProductGroup> ProductGroupRepository
            => _productGroupRepository ?? (_productGroupRepository = new Repository<ProductGroup>(_context));

        public IRepository<ProductTab> ProductTabRepository
            => _productTabRepository ?? (_productTabRepository = new Repository<ProductTab>(_context));

        public IRepository<SalesOrder> SalesOrderRepository
            => _salesOrderRepository ?? (_salesOrderRepository = new Repository<SalesOrder>(_context));

        public IRepository<Transaction> TransactionRepository
            => _transactionRepository ?? (_transactionRepository = new Repository<Transaction>(_context));

        public UnitOfWork(CashRegisterContext context, DalFacade controller)
        {
            _context = context;
            _controller = controller;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool diposing)
        {
            if (!_disposed)
            {
                if (diposing)
                {
                    _context.Dispose();
                    _controller.ReturnUnitOfWork();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}