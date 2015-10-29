

namespace CashRegister.Database
{
    using System.Data.Entity.ModelConfiguration;
    using Models;

    namespace CashRegister.Database
    {
        public class OrderLineEntityConfiguration : EntityTypeConfiguration<OrderLine>
        {
            public OrderLineEntityConfiguration()
            {
                HasKey(e => e.Id);

                Property(p => p.Quantity)
                    .IsRequired();

                HasRequired(e => e.Product);

                Property(p => p.UnitPrice)
                    .IsRequired();

                HasRequired(e => e.Discount);

                Property(p => p.DiscountValue)
                    .IsRequired();

                HasRequired(e => e.SalesOrder)
                    .WithMany(e => e.Lines);
            }
        }
    }

}