using System.Data.Entity.ModelConfiguration;
using CashRegister.WebApi.Models;

namespace CashRegister.Database.Configurations
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
                .WithMany();
        }
    }
}