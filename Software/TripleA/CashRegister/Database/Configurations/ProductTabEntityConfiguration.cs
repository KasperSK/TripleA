using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using CashRegister.Models;

namespace CashRegister.Database.Configurations
{
    public class ProductTabEntityConfiguration : EntityTypeConfiguration<ProductTab>
    {
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