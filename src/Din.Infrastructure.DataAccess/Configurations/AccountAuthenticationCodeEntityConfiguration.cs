using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class AccountAuthorizationCodeEntityConfiguration : IEntityTypeConfiguration<AccountAuthorizationCode>
    {
        public void Configure(EntityTypeBuilder<AccountAuthorizationCode> builder)
        {
            builder.ToTable("account_authorization_code");

            builder.HasKey(a => a.Id)
                .HasName("id");

            builder.Property(a => a.AccountId)
                .HasColumnName("account_id")
                .IsRequired();

            builder.Property(a => a.Generated)
                .HasColumnName("generated")
                .IsRequired();

            builder.Property(a => a.Code)
                .HasColumnName("code")
                .IsRequired();

            builder.Property(a => a.Active)
                .HasColumnName("active")
                .IsRequired();
        }
    }
}
