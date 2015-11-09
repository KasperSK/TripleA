using System.Data.Entity.ModelConfiguration;
using CashRegister.Models;

namespace CashRegister.Database.Configurations
{
    public class DiscountEntityConfiguration : EntityTypeConfiguration<Discount>
    {
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