using System.Data.Entity.ModelConfiguration;
using CashRegister.Models;

namespace CashRegister.Database.Configurations
{
    public class PaymentTypeEntityConfiguration : EntityTypeConfiguration<PaymentType>
    {
        public PaymentTypeEntityConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.Description)
                .IsRequired();
        }
    }
}