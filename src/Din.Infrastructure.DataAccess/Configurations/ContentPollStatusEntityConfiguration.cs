using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class ContentPollStatusEntityConfiguration : IEntityTypeConfiguration<ContentPollStatus>
    {
        public void Configure(EntityTypeBuilder<ContentPollStatus> builder)
        {
            builder.ToTable("content_poll_status");

            builder.HasKey(c => c.Id)
                .HasName("id");

            builder.Property(c => c.ContentId)
                .HasColumnName("content_id")
                .IsRequired();

            builder.Property(c => c.PlexUrlPolled)
                .HasColumnName("plex_url_polled");

            builder.Property(c => c.PosterUrlPolled)
                .HasColumnName("poster_url_polled");
        }
    }
}
