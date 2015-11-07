using System.Data.Entity.ModelConfiguration;
using CashRegister.Models;

namespace CashRegister.Database.Configurations
{
    public class OrderStatusEntityConfiguration : EntityTypeConfiguration<OrderStatus>
    {
        public OrderStatusEntityConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.Name)
                .IsRequired();
        }
    }
}