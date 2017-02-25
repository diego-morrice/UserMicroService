using System.Data.Entity.ModelConfiguration;
using Domain.User.Entities;

namespace Infrastructure.Data.User.Mapping
{
    public class AddressMap : ComplexTypeConfiguration<Address>
    {
        public AddressMap()
        {
            Property(t => t.AddressLineOne)
                .IsOptional()
                .HasMaxLength(150);

            Property(t => t.AddressLineTwo)
                .IsOptional()
                .HasMaxLength(150);

            Property(t => t.City)
                .IsOptional()
                .HasMaxLength(50);

            Property(t => t.Country)
                .IsOptional()
                .HasMaxLength(50);

            Property(t => t.Number)
                .IsOptional()
                .HasMaxLength(5);

            Property(t => t.State)
                .IsOptional()
                .HasMaxLength(50);

            Property(t => t.ZipCode)
                .IsOptional()
                .HasMaxLength(15);
        }
    }
}