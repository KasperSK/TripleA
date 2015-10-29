

namespace CashRegister.Database
{
    using System.Data.Entity.ModelConfiguration;
    using Models;

    namespace CashRegister.Database
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
                    .WithMany()
                    .Map(m =>
                    {
                        m.ToTable("ProductGroup_Discount");
                        m.MapLeftKey("DiscountId");
                        m.MapRightKey("ProductGroupId");
                    });
            }
        }
    }

}