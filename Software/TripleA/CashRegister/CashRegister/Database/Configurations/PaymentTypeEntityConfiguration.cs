namespace CashRegister.Database.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using Models;

    namespace CashRegister.Database
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

}