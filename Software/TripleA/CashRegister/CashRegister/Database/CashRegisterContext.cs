namespace CashRegister.Database
{
    using System.Data.Entity;

    using CashRegister.Database;
    using Configurations;
    using Models;

    public partial class CashRegisterContext : DbContext
    {
        public CashRegisterContext()
            : base("name=CashRegisterContext")
        {
            Database.SetInitializer(new CashRegisterInitializer());
        }

        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<Transaction> Transaktions { get; set; }
        public virtual DbSet<ProductTab> ProductTabs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DiscountEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductGroupEntityConfiguration());
            modelBuilder.Configurations.Add(new OrderLineEntityConfiguration());
            modelBuilder.Configurations.Add(new SalesOrderEntityConfiguration());
            modelBuilder.Configurations.Add(new TransactionEntityConfiguration());
            modelBuilder.Configurations.Add(new PaymentTypeEntityConfiguration());
            modelBuilder.Configurations.Add(new OrderStatusEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductTabEntityConfiguration());
        }
    }
}
