using System;
using CashRegister.Database;
using CashRegister.Models;

namespace CashRegister.Dal
{
    /// <summary>
    /// Implemetation of unit of work
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Database context for the repositories to work on
        /// </summary>
        private readonly CashRegisterContext _context;

        /// <summary>
        /// The facade using this unit of work
        /// </summary>
        private readonly IDalFacade _controller;

        /// <summary>
        /// Internal representaion of the repository
        /// </summary>
        private IRepository<Discount> _discountRepository;

        /// <summary>
        /// To indicate if we are diposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Internal representaion of the repository
        /// </summary>
        private IRepository<OrderLine> _orderLineRepository;

        /// <summary>
        /// Internal representaion of the repository
        /// </summary>
        private IRepository<ProductGroup> _productGroupRepository;

        /// <summary>
        /// Internal representaion of the repository
        /// </summary>
        private IRepository<Product> _productRepository;

        /// <summary>
        /// Internal representaion of the repository
        /// </summary>
        private IRepository<ProductTab> _productTabRepository;

        /// <summary>
        /// Internal representaion of the repository
        /// </summary>
        private IRepository<ProductType> _productTypeRepository;

        /// <summary>
        /// Internal representaion of the repository
        /// </summary>
        private IRepository<SalesOrder> _salesOrderRepository;

        /// <summary>
        /// Internal representaion of the repository
        /// </summary>
        private IRepository<Transaction> _transactionRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The database context to work on</param>
        /// <param name="controller">The facade that owns this unit of work</param>
        public UnitOfWork(CashRegisterContext context, IDalFacade controller)
        {
            _context = context;
            _controller = controller;
        }

        /// <summary>
        /// Returns the internal repository if it exists else it creates a new one
        /// </summary>
        public IRepository<Discount> DiscountRepository
            => _discountRepository ?? (_discountRepository = new Repository<Discount>(_context));

        /// <summary>
        /// Returns the internal repository if it exists else it creates a new one
        /// </summary>
        public IRepository<OrderLine> OrderLineRepository
            => _orderLineRepository ?? (_orderLineRepository = new Repository<OrderLine>(_context));

        /// <summary>
        /// Returns the internal repository if it exists else it creates a new one
        /// </summary>
        public IRepository<Product> ProductRepository
            => _productRepository ?? (_productRepository = new Repository<Product>(_context));

        /// <summary>
        /// Returns the internal repository if it exists else it creates a new one
        /// </summary>
        public IRepository<ProductGroup> ProductGroupRepository
            => _productGroupRepository ?? (_productGroupRepository = new Repository<ProductGroup>(_context));

        /// <summary>
        /// Returns the internal repository if it exists else it creates a new one
        /// </summary>
        public IRepository<ProductType> ProductTypeRepository
            => _productTypeRepository ?? (_productTypeRepository = new Repository<ProductType>(_context));

        /// <summary>
        /// Returns the internal repository if it exists else it creates a new one
        /// </summary>
        public IRepository<ProductTab> ProductTabRepository
            => _productTabRepository ?? (_productTabRepository = new Repository<ProductTab>(_context));

        /// <summary>
        /// Returns the internal repository if it exists else it creates a new one
        /// </summary>
        public IRepository<SalesOrder> SalesOrderRepository
            => _salesOrderRepository ?? (_salesOrderRepository = new Repository<SalesOrder>(_context));

        /// <summary>
        /// Returns the internal repository if it exists else it creates a new one
        /// </summary>
        public IRepository<Transaction> TransactionRepository
            => _transactionRepository ?? (_transactionRepository = new Repository<Transaction>(_context));

        /// <summary>
        /// To save the changes to the database context
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// To dipose of the unit of work
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Internal dispose makes sure to dispose the context and return the unit of work to the facade
        /// </summary>
        /// <param name="disposing"></param>
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