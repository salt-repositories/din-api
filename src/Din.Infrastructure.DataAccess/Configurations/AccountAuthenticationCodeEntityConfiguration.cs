using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class AccountAuthenticationCodeEntityConfiguration : IEntityTypeConfiguration<AccountAuthenticationCode>
    {
        public void Configure(EntityTypeBuilder<AccountAuthenticationCode> builder)
        {
            builder.ToTable("account_authentication_code");

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
