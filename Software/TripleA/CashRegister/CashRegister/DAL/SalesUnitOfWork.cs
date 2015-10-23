using System;
using CashRegister.Database;
using CashRegister.DAL;

namespace CashRegister.DAL
{
    public class SalesUnitOfWork : IDisposable
    {
        private readonly CashRegisterContext Context;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<Price> _priceRepository;
        private GenericRepository<OrderList> _OrderListRepository;
        private GenericRepository<Status> _StatuspRepository;

        public SalesUnitOfWork(CashRegisterContext context)
        {
            Context = context;
        }

        public GenericRepository<Product> ProductRepository
            => _productRepository ?? (_productRepository = new GenericRepository<Product>(Context));

        public GenericRepository<Price> PriceRepository
            => _priceRepository ?? (_priceRepository = new GenericRepository<Price>(Context));

        public GenericRepository<OrderList> OrderListRepository
            => _OrderListRepository ?? (_OrderListRepository = new GenericRepository<OrderList>(Context));

        public GenericRepository<Status> StatuspRepository
            => _StatuspRepository ?? (_StatuspRepository = new GenericRepository<Status>(Context));

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