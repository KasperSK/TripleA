namespace CashRegister.Database.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;
    using Models;

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

            HasMany(p => p.ProductGroups)
                .WithMany();
        }
    }
}