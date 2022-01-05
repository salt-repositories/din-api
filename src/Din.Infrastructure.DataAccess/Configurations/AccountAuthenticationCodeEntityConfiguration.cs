using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class AccountAuthorizationCodeEntityConfiguration : EntityConfiguration<AccountAuthorizationCode>
    {
        public override void Configure(EntityTypeBuilder<AccountAuthorizationCode> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Generated)
                .HasColumnName("generated")
                .IsRequired();

            builder.Property(a => a.Code)
                .HasColumnName("code")
                .IsRequired();

            builder.Property(a => a.Active)
                .HasColumnName("active")
                .IsRequired();
            
            builder.HasOne(ac => ac.Account)
                .WithMany(a => a.Codes)
                .HasForeignKey(ac => ac.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
