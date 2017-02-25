using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.Data.User.Mapping
{
    public class GoogleUserMap : EntityTypeConfiguration<Domain.User.Entities.GoogleUser>
    {
        public GoogleUserMap()
        {
            HasKey(t => t.Id)
                .Property(t => t.Id).
                HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(t => t.User).WithOptional(t => t.GoogleUser);
        }
    }
}