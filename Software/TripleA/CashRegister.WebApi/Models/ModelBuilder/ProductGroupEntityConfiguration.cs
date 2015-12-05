using System.Data.Entity.ModelConfiguration;

namespace CashRegister.WebApi.Models.ModelBuilder
{
    /// <summary>
    /// Class to setup the Db
    /// </summary>
    public class ProductGroupEntityConfiguration : EntityTypeConfiguration<ProductGroup>
    {
        /// <summary>
        /// The configuration
        /// </summary>
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