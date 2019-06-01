using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class LoginLocationEntityConfiguration : IEntityTypeConfiguration<LoginLocation>
    {
        public void Configure(EntityTypeBuilder<LoginLocation> builder)
        {
            builder.ToTable("login_location");

            builder.HasKey(ll => ll.Id)
                .HasName("id");

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
