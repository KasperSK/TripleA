

namespace CashRegister.Database
{
    using System.Data.Entity.ModelConfiguration;
    using Models;

    namespace CashRegister.Database
    {
        public class ProductEntityConfiguration : EntityTypeConfiguration<Product>
        {
            public ProductEntityConfiguration()
            {
                HasKey(e => e.Id);

                Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                Property(p => p.Price)
                    .IsRequired();

                Property(p => p.Saleable)
                    .IsRequired();

                HasMany(e => e.ProductGroups)
                    .WithMany();
            }
        }
    }

}