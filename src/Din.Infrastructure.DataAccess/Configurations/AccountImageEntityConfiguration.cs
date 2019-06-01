using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class AccountImageEntityConfiguration : IEntityTypeConfiguration<AccountImage>
    {
        public void Configure(EntityTypeBuilder<AccountImage> builder)
        {
            builder.ToTable("account_image");

            builder.HasKey(ai => ai.Id)
                .HasName("id");

            builder.Property(ai => ai.AccountId)
                .HasColumnName("account_id")
                .IsRequired();

            builder.Property(ai => ai.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(ai => ai.Data)
                .HasColumnName("data")
                .IsRequired();
        }
    }
}
