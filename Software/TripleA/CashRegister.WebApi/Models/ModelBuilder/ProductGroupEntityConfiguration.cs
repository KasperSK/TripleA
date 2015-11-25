using System.Data.Entity.ModelConfiguration;

namespace CashRegister.WebApi.Models.ModelBuilder
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
                .WithMany(e => e.ProductGroups);
        }
    }
}