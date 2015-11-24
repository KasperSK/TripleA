using System.Data.Entity;
using CashRegister.WebApi.Models.ModelBuilder;


namespace CashRegister.WebApi.Models
{
    public class CashRegisterContext : DbContext
    {
        public CashRegisterContext()
            : base("name=CashRegisterContext")
        {
        }

        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<ProductTab> ProductTabs { get; set; }

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