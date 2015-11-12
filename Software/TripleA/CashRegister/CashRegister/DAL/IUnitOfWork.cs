using System;
using CashRegister.Models;

namespace CashRegister.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Discount> DiscountRepository { get; } 
        IRepository<OrderLine> OrderLineRepository { get; }
        IRepository<OrderStatus> OrderStatusRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<ProductGroup> ProductGroupRepository { get; }
        IRepository<ProductTab> ProductTabRepository { get; }
        IRepository<SalesOrder> SalesOrderRepository { get; }
        IRepository<Transaction> TransactionRepository { get; }
               
        void Save();
    }
}