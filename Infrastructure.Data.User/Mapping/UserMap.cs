using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.Data.User.Mapping
{
    public class UserMap : EntityTypeConfiguration<Domain.User.Entities.User>
    {
        public UserMap()
        {
            HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Active)
                .IsRequired();

            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute {IsUnique = true}));

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute {IsUnique = true}));

            Property(t => t.Password)               
                .HasMaxLength(20);

            HasMany(t => t.AutenticationToken).WithRequired(t => t.User);
            HasOptional(t => t.FacebookUser).WithRequired(t => t.User);
            HasOptional(t => t.GoogleUser).WithRequired(t => t.User);
        }
    }
}