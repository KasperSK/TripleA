namespace CashRegister.Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CashRegisterContext : DbContext
    {
        public CashRegisterContext()
            : base("name=CashRegisterContext")
        {
            Database.SetInitializer(new CashRegisterInitializer());
        }

        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<OrderList> OrderLists { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<ProductSubGroup> ProductSubGroups { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Transaktion> Transaktions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderList>()
                .HasMany(e => e.Transaktions)
                .WithRequired(e => e.OrderList)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderList>()
                .HasMany(e => e.Products)
                .WithMany(e => e.OrderLists)
                .Map(m => m.ToTable("OrderList_Product").MapLeftKey("OrderId").MapRightKey("ProductId"));

            modelBuilder.Entity<PaymentType>()
                .HasMany(e => e.Transaktions)
                .WithRequired(e => e.PaymentType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Prices)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductGroups)
                .WithMany(e => e.Products)
                .Map(m => m.ToTable("ProductGroup_Product").MapLeftKey("ProductId").MapRightKey("ProductGroupId"));

            modelBuilder.Entity<Status>()
                .HasMany(e => e.OrderLists)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);
        }
    }
}
