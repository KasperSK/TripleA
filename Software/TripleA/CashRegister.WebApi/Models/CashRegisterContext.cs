using System.Data.Entity;
using CashRegister.WebApi.Models.ModelBuilder;


namespace CashRegister.WebApi.Models
{
    /// <summary>
    /// The database context
    /// </summary>
    public class CashRegisterContext : DbContext
    {
        /// <summary>
        /// To initialize a new context
        /// </summary>
        public CashRegisterContext()
            : base("name=CashRegisterContext")
        {
        }


        #region Dbsets
        /// <summary>
        /// Dbset for discount
        /// </summary>
        public virtual DbSet<Discount> Discounts { get; set; }
        /// <summary>
        /// Dbset for salesorders
        /// </summary>
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        /// <summary>
        /// Dbset for orderlines
        /// </summary>
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        /// <summary>
        /// Dbset for products
        /// </summary>
        public virtual DbSet<Product> Products { get; set; }
        /// <summary>
        /// Dbset for product groups
        /// </summary>
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        /// <summary>
        /// Dbset for ProductTypes
        /// </summary>
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        /// <summary>
        /// Dbset for Transactions
        /// </summary>
        public virtual DbSet<Transaction> Transactions { get; set; }
        /// <summary>
        /// Dbset for ProductTabs
        /// </summary>
        public virtual DbSet<ProductTab> ProductTabs { get; set; }
        #endregion

        /// <summary>
        /// Specification on how to build database
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DiscountEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductGroupEntityConfiguration());
            modelBuilder.Configurations.Add(new OrderLineEntityConfiguration());
            modelBuilder.Configurations.Add(new SalesOrderEntityConfiguration());
            modelBuilder.Configurations.Add(new TransactionEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductTabEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductTypeEntityConfiguration());
        }
    }
}