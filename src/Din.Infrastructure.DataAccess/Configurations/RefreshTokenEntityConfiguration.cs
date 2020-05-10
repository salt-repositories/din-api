using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("account_refresh_token");

            builder.HasKey(r => r.Id)
                .HasName("id");

            builder.Property(r => r.Token)
                .HasColumnName("token")
                .IsRequired();

            builder.Property(r => r.AccountIdentity)
                .HasColumnName("account_id")
                .IsRequired();

            builder.Property(r => r.CreationDate)
                .HasColumnName("creation_date")
                .IsRequired();
        }
    }
}
