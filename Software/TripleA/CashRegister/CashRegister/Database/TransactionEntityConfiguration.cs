

namespace CashRegister.Database
{
    using System.Data.Entity.ModelConfiguration;
    using Models;

    namespace CashRegister.Database
    {
        public class TransactionEntityConfiguration : EntityTypeConfiguration<Transaction>
        {
            public TransactionEntityConfiguration()
            {
                HasKey(p => p.Id);

                Property(p => p.Price)
                    .IsRequired();

                Property(p => p.Date)
                    .IsRequired();

                HasRequired(e => e.Paymenttype);

                HasRequired(e => e.SalesOrder)
                    .WithMany(e => e.Transactions);
                
            }
        }
    }

}