using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.Data.User.Mapping
{
    public class AutenticateTokenMap : EntityTypeConfiguration<Domain.User.Entities.AutenticateToken>
    {
        public AutenticateTokenMap()
        {
            HasKey(t => t.Id)
            .Property(t => t.Id)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Token)
                .IsRequired();

            Property(t => t.Active)
                .IsRequired();

            HasRequired(t => t.User).WithMany(t => t.AutenticationToken).HasForeignKey(t => t.IdUser);
        }
    }
}
