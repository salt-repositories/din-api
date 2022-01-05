using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class ContentRatingEntityConfiguration : EntityConfiguration<ContentRating>
    {
        public override void Configure(EntityTypeBuilder<ContentRating> builder)
        {
            base.Configure(builder);
            
            builder.Property(c => c.MovieId)
                .HasColumnName("movie_id")
                .IsRequired(false);
            
            builder.Property(c => c.TvShowId)
                .HasColumnName("tvshow_id")
                .IsRequired(false);
            
            builder.Property(c => c.Votes)
                .HasColumnName("votes")
                .IsRequired();

            builder.Property(c => c.Value)
                .HasColumnName("value")
                .IsRequired();
        }
    }
}
