using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class ContentRatingEntityConfiguration : IEntityTypeConfiguration<ContentRating>
    {
        public void Configure(EntityTypeBuilder<ContentRating> builder)
        {
            builder.ToTable("content_rating");

            builder.HasKey(c => c.Id)
                .HasName("id");

            builder.Property(c => c.ContentId)
                .HasColumnName("content_id");

            builder.Property(c => c.Votes)
                .HasColumnName("votes")
                .IsRequired();

            builder.Property(c => c.Value)
                .HasColumnName("value")
                .IsRequired();
        }
    }
}
