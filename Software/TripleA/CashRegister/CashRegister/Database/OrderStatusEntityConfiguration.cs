

namespace CashRegister.Database
{
    using System.Data.Entity.ModelConfiguration;
    using Models;

    namespace CashRegister.Database
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

}