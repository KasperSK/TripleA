using System.Data.Entity.ModelConfiguration;

namespace CashRegister.WebApi.Models.ModelBuilder
{
    /// <summary>
    /// Class to setup the Db
    /// </summary>
    public class ProductEntityConfiguration : EntityTypeConfiguration<Product>
    {
        /// <summary>
        /// The configuration
        /// </summary>
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
                .WithMany(e => e.Products);
           
        }
    }
}