using System;
using CashRegister.Models;

namespace CashRegister.Dal
{
    /// <summary>
    /// Unit of work interface with repositories defined.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Unit of work must have a Discount repository
        /// </summary>
        IRepository<Discount> DiscountRepository { get; }

        /// <summary>
        /// Unit of work must have a OrderLine repository
        /// </summary>
        IRepository<OrderLine> OrderLineRepository { get; }

        /// <summary>
        /// Unit of work must have a Product repository
        /// </summary>
        IRepository<Product> ProductRepository { get; }

        /// <summary>
        /// Unit of work must have a ProductGroup repository
        /// </summary>
        IRepository<ProductGroup> ProductGroupRepository { get; }

        /// <summary>
        /// Unit of work must have a ProductType repository
        /// </summary>
        IRepository<ProductType> ProductTypeRepository { get; }

        /// <summary>
        /// Unit of work must have a ProductTab repository
        /// </summary>
        IRepository<ProductTab> ProductTabRepository { get; }

        /// <summary>
        /// Unit of work must have a SalesOrder repository
        /// </summary>
        IRepository<SalesOrder> SalesOrderRepository { get; }

        /// <summary>
        /// Unit of work must have a Transaction repository
        /// </summary>
        IRepository<Transaction> TransactionRepository { get; }

        /// <summary>
        /// To save the changes made to the repositories
        /// </summary>
        void Save();
    }
}