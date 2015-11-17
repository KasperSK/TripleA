using System.Data.Entity.ModelConfiguration;
using CashRegister.Models;

namespace CashRegister.Database.Configurations
{
    public class ProductTypeEntityConfiguration : EntityTypeConfiguration<ProductType>
    {
        public ProductTypeEntityConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.Name)
                .IsRequired();

            Property(p => p.Color)
                .IsRequired();

            Property(p => p.Price)
                .IsRequired();

            HasMany(p => p.ProductGroups)
                .WithMany();
        }
    }
}