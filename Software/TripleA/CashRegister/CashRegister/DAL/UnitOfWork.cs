using System;
using CashRegister.Database;
using CashRegister.Models;

namespace CashRegister.DAL
{
    public class ProductUnitOfWork : IDisposable
    {
        private readonly CashRegisterContext Context;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<ProductGroup> _productGroupRepository;

        public ProductUnitOfWork(CashRegisterContext context)
        {
            Context = context;
        }

        public GenericRepository<Product> ProductRepository => _productRepository ?? (_productRepository = new GenericRepository<Product>(Context));

        public GenericRepository<ProductGroup> ProductGroupRepository => _productGroupRepository ?? (_productGroupRepository = new GenericRepository<ProductGroup>(Context));

        public void Save()
        {
            Context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool diposing)
        {
            if (!_disposed)
            {
                if (diposing)
                {
                    Context.Dispose();
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

    public class OrderUnitOfWork
    {
        private readonly CashRegisterContext Context;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<SalesOrder> _SalesOrderRepository;

        public OrderUnitOfWork(CashRegisterContext context)
        {
            Context = context;
        }

        public GenericRepository<Product> ProductRepository => _productRepository ?? (_productRepository = new GenericRepository<Product>(Context));

        public GenericRepository<SalesOrder> SalesOrderRepository
            => _SalesOrderRepository ?? (_SalesOrderRepository = new GenericRepository<SalesOrder>(Context)); 

        public void Save()
        {
            Context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool diposing)
        {
            if (!_disposed)
            {
                if (diposing)
                {
                    Context.Dispose();
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