using System.Data.Entity.ModelConfiguration;

namespace CashRegister.WebApi.Models.ModelBuilder
{
    /// <summary>
    /// Class to setup the Db
    /// </summary>
    public class SalesOrderEntityConfiguration : EntityTypeConfiguration<SalesOrder>
    {
        /// <summary>
        /// The configuration
        /// </summary>
        public SalesOrderEntityConfiguration()
        {
            HasKey(e => e.Id);

            Property(p => p.Date)
                .IsRequired();

            Property(p => p.Status)
                .IsRequired();

            HasMany(e => e.Transactions)
                .WithRequired(p => p.SalesOrder);

            HasMany(e => e.Lines);
        }
    }
}