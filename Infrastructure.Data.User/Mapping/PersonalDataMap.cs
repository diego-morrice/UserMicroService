using System.Data.Entity.ModelConfiguration;
using Domain.User.Entities;

namespace Infrastructure.Data.User.Mapping
{
    public class PersonalDataMap : ComplexTypeConfiguration<PersonalData>
    {
        public PersonalDataMap()
        {           

            Property(t => t.CountryCode)
                .IsOptional()
                .HasMaxLength(3);

            Property(t => t.FullName)
                .IsOptional()
                .HasMaxLength(150);

            Property(t => t.Gender)
                .IsOptional();              

            Property(t => t.PhoneNumber)
                .IsOptional()
                .HasMaxLength(15);
        }
    }
}