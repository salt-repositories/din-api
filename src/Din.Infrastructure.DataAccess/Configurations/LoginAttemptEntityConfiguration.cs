using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class LoginAttemptEntityConfiguration : EntityConfiguration<LoginAttempt>
    {
        public override void Configure(EntityTypeBuilder<LoginAttempt> builder)
        {
            base.Configure(builder);

            builder.Property(la => la.Username)
                .HasColumnName("username")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(la => la.Device)
                .HasColumnName("device")
                .IsRequired();

            builder.Property(la => la.Os)
                .HasColumnName("os")
                .IsRequired();

            builder.Property(la => la.Browser)
                .HasColumnName("browser")
                .IsRequired();

            builder.Property(la => la.PublicIp)
                .HasColumnName("public_ip")
                .IsRequired();

            builder.Property(la => la.LocationId)
                .HasColumnName("location_id");

            builder.Property(la => la.DateAndTime)
                .HasColumnName("date")
                .IsRequired();

            builder.Property(la => la.Status)
                .HasColumnName("status")
                .IsRequired()
                .HasConversion(new EnumToStringConverter<LoginStatus>());

            builder.HasOne(la => la.Location)
                .WithMany(ll => ll.LoginAttempts)
                .HasForeignKey(ll => ll.LocationId);
        }
    }
}
