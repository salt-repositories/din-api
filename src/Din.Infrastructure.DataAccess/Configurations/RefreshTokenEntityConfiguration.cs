using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class RefreshTokenEntityConfiguration : EntityConfiguration<RefreshToken>
    {
        public override void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            base.Configure(builder);

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
