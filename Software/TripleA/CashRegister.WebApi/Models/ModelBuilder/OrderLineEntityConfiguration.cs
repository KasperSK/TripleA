using System.Data.Entity.ModelConfiguration;

namespace CashRegister.WebApi.Models.ModelBuilder
{
    /// <summary>
    /// Class to setup the Db
    /// </summary>
    public class OrderLineEntityConfiguration : EntityTypeConfiguration<OrderLine>
    {
        /// <summary>
        /// The configuration
        /// </summary>
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