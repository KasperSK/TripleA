using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Database;

namespace CashRegister.DAL
{
    public class ProductUnitOfWork : IDisposable
    {
        private readonly CashRegisterContext Context;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<Price> _priceRepository;
        private GenericRepository<ProductGroup> _productGroupRepository;

        public ProductUnitOfWork(CashRegisterContext context)
        {
            Context = context;
        }

        public GenericRepository<Product> ProductRepository => _productRepository ?? (_productRepository = new GenericRepository<Product>(Context));

        public GenericRepository<Price> PriceRepository => _priceRepository ?? (_priceRepository = new GenericRepository<Price>(Context));

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
        private GenericRepository<Price> _priceRepository;
        private GenericRepository<OrderList> _orderListRepository;

        public OrderUnitOfWork(CashRegisterContext context)
        {
            Context = context;
        }

        public GenericRepository<Product> ProductRepository => _productRepository ?? (_productRepository = new GenericRepository<Product>(Context));

        public GenericRepository<Price> PriceRepository => _priceRepository ?? (_priceRepository = new GenericRepository<Price>(Context));

        public GenericRepository<OrderList> OrderListRepository
            => _orderListRepository ?? (_orderListRepository = new GenericRepository<OrderList>(Context)); 

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