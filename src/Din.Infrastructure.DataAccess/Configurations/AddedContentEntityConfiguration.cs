using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class AddedContentEntityConfiguration : ScopedEntityConfiguration<AddedContent>
    {
        public AddedContentEntityConfiguration(DinContext context) : base(context)
        {
        }
        
        public override void Configure(EntityTypeBuilder<AddedContent> builder)
        {
            base.Configure(builder);

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
        }
    }
}
