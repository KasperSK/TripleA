using System.Data.Entity.ModelConfiguration;
using CashRegister.WebApi.Models;

namespace CashRegister.Database.Configurations
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

            HasRequired(p => p.Discount);

            Property(p => p.DiscountValue)
                .IsRequired();

            HasRequired(e => e.SalesOrder);
        }
    }
}