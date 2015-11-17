using System.Data.Entity.ModelConfiguration;
using CashRegister.Models;

namespace CashRegister.Database.Configurations
{
    public class SalesOrderEntityConfiguration : EntityTypeConfiguration<SalesOrder>
    {
        public SalesOrderEntityConfiguration()
        {
            HasKey(e => e.Id);

            Property(p => p.Date)
                .IsRequired();

            Property(p => p.Total)
                .IsRequired();

            HasRequired(e => e.Status);

            HasMany(e => e.Transactions)
                .WithRequired(e => e.SalesOrder);

            HasMany(e => e.Lines)
                .WithRequired(e => e.SalesOrder);
        }
    }
}