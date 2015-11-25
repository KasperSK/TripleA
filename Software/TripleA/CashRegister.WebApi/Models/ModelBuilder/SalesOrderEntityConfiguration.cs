﻿using System.Data.Entity.ModelConfiguration;

namespace CashRegister.WebApi.Models.ModelBuilder
{
    public class SalesOrderEntityConfiguration : EntityTypeConfiguration<SalesOrder>
    {
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