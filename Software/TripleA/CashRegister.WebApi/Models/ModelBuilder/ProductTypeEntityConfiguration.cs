using System.Data.Entity.ModelConfiguration;

namespace CashRegister.WebApi.Models.ModelBuilder
{
    /// <summary>
    /// Class to setup the Db
    /// </summary>
    public class ProductTypeEntityConfiguration : EntityTypeConfiguration<ProductType>
    {
        /// <summary>
        /// The configuration
        /// </summary>
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