using System;
using CashRegister.Database;
using CashRegister.DAL;
using CashRegister.Models;

namespace CashRegister.DAL
{
    public class SalesUnitOfWork : IDisposable
    {
        private readonly CashRegisterContext Context;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<SalesOrder> _SalesOrderRepository;
        private GenericRepository<OrderStatus> _StatuspRepository;

        public SalesUnitOfWork(CashRegisterContext context)
        {
            Context = context;
        }

        public GenericRepository<Product> ProductRepository
            => _productRepository ?? (_productRepository = new GenericRepository<Product>(Context));

       
        public GenericRepository<SalesOrder> SalesOrderRepository
            => _SalesOrderRepository ?? (_SalesOrderRepository = new GenericRepository<SalesOrder>(Context));

        public GenericRepository<OrderStatus> StatuspRepository
            => _StatuspRepository ?? (_StatuspRepository = new GenericRepository<OrderStatus>(Context));

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