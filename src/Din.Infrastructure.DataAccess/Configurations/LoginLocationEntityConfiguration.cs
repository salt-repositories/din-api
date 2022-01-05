using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class LoginLocationEntityConfiguration : EntityConfiguration<LoginLocation>
    {
        public override void Configure(EntityTypeBuilder<LoginLocation> builder)
        {
            base.Configure(builder);

            builder.Property(ll => ll.ContinentCode)
                .HasColumnName("continent_code");

            builder.Property(ll => ll.ContinentName)
                .HasColumnName("continent_name");

            builder.Property(ll => ll.CountryCode)
                .HasColumnName("country_code");

            builder.Property(ll => ll.CountryName)
                .HasColumnName("country_name");

            builder.Property(ll => ll.RegionCode)
                .HasColumnName("region_code");

            builder.Property(ll => ll.RegionName)
                .HasColumnName("region_name");

            builder.Property(ll => ll.City)
                .HasColumnName("city");

            builder.Property(ll => ll.ZipCode)
                .HasColumnName("zip_code");

            builder.Property(ll => ll.Latitude)
                .HasColumnName("latitude");

            builder.Property(ll => ll.Longitude)
                .HasColumnName("longitude");
        }
    }
}
