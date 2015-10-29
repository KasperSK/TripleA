

namespace CashRegister.Database
{
    using System.Data.Entity.ModelConfiguration;
    using Models;

    namespace CashRegister.Database
    {
        public class ProductGroupEntityConfiguration : EntityTypeConfiguration<ProductGroup>
        {
            public ProductGroupEntityConfiguration()
            {
                HasKey(e => e.Id);

                Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                HasMany(e => e.Products)
                    .WithMany()
                    .Map(m =>
                    {
                        m.ToTable("ProductGroup_Product");
                        m.MapLeftKey("ProductId");
                        m.MapRightKey("ProductGroupId");
                    });
            }
        }
    }

}