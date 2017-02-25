using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.Data.User.Mapping
{
    public class FacebookUserMap : EntityTypeConfiguration<Domain.User.Entities.FacebookUser>
    {
        public FacebookUserMap()
        {
            HasKey(t => t.Id)
                .Property(t => t.Id).
                HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(t => t.User).WithOptional(t => t.FacebookUser);
        }

    }
}
