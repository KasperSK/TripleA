using System.Data.Entity.ModelConfiguration;

namespace CashRegister.WebApi.Models.ModelBuilder
{
    /// <summary>
    /// Class to setup the Db
    /// </summary>
    public class DiscountEntityConfiguration : EntityTypeConfiguration<Discount>
    {
        /// <summary>
        /// The configuration
        /// </summary>
        public DiscountEntityConfiguration()
        {
            HasKey(e => e.Id);

            Property(p => p.Description)
                .HasMaxLength(200)
                .IsRequired();

            Property(p => p.Percent)
                .IsRequired();

            HasMany(e => e.ProductGroups)
                .WithMany();
        }
    }
}