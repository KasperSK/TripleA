using System.Data.Entity.ModelConfiguration;

namespace CashRegister.WebApi.Models.ModelBuilder
{
    public class TransactionEntityConfiguration : EntityTypeConfiguration<Transaction>
    {
        public TransactionEntityConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.Price)
                .IsRequired();

            Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.Date)
                .IsRequired();

            Property(e => e.PaymentType)
                .IsRequired();

            Property(p => p.Status)
                .IsRequired();

            HasRequired(e => e.SalesOrder)
                .WithMany(p => p.Transactions);
        }
    }
}