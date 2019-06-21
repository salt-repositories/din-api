using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("account");

            builder.HasKey(a => a.Id)
                .HasName("id");

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
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.Codes)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
