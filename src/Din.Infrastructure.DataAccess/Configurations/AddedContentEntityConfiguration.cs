using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class AddedContentEntityConfiguration : IEntityTypeConfiguration<AddedContent>
    {
        public void Configure(EntityTypeBuilder<AddedContent> builder)
        {
            builder.ToTable("added_content");

            builder.HasKey(ac => ac.Id)
                .HasName("id");

            builder.Property(ai => ai.SystemId)
                .HasColumnName("system_id")
                .IsRequired();

            builder.Property(ac => ac.ForeignId)
                .HasColumnName("foreign_id")
                .IsRequired();

            builder.Property(ac => ac.Title)
                .HasColumnName("title")
                .IsRequired();

            builder.Property(ac => ac.Type)
                .HasColumnName("type")
                .IsRequired()
                .HasConversion(new EnumToStringConverter<ContentType>());

            builder.Property(ac => ac.DateAdded)
                .HasColumnName("date_added")
                .IsRequired();

            builder.Property(ac => ac.Status)
                .HasColumnName("status")
                .IsRequired()
                .HasConversion(new EnumToStringConverter<ContentStatus>());

            builder.Property(ac => ac.AccountId)
                .HasColumnName("account_id")
                .IsRequired();
        }
    }
}
