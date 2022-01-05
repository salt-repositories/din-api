using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class ContentPollStatusEntityConfiguration : EntityConfiguration<ContentPollStatus>
    {
        public override void Configure(EntityTypeBuilder<ContentPollStatus> builder)
        {
            base.Configure(builder);

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
