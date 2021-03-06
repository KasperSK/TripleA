﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CashRegister.WebApi.Models.ModelBuilder
{
    /// <summary>
    /// Class to setup the Db
    /// </summary>
    public class ProductTabEntityConfiguration : EntityTypeConfiguration<ProductTab>
    {
        /// <summary>
        /// The configuration
        /// </summary>
        public ProductTabEntityConfiguration()
        {
            HasKey(e => e.Id);

            Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            Property(p => p.Active)
                .IsRequired();

            Property(p => p.Color)
                .IsRequired();

            Property(p => p.Priority)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute
                        {
                            IsUnique = true
                        }
                        )
                );

            HasMany(p => p.ProductTypes)
                .WithMany();
        }
    }
}