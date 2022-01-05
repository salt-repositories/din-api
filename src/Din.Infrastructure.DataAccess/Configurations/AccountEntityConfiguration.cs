using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class AccountEntityConfiguration : EntityConfiguration<Account>
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            base.Configure(builder);
            
            builder.Property(a => a.Username)
                .HasColumnName("username")
                .IsRequired()
                .HasMaxLength(40);
            builder.HasIndex(a => a.Username)
                .IsUnique();

            builder.Property(a => a.Email)
                .HasColumnName("email")
                .IsRequired();
            builder.HasIndex(a => a.Email)
                .IsUnique();

            builder.Property(a => a.Hash)
                .HasColumnName("hash")
                .IsRequired();

            builder.Property(a => a.Role)
                .HasColumnName("account_role")
                .IsRequired()
                .HasConversion(new EnumToStringConverter<AccountRole>());

            builder.Property(a => a.Active)
                .HasColumnName("active")
                .IsRequired();

            builder.HasOne(a => a.Image)
                .WithOne()
                .HasForeignKey<AccountImage>(ai => ai.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.AddedContent)
                .WithOne(ac => ac.Account)
                .HasForeignKey(ac => ac.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.Codes)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
