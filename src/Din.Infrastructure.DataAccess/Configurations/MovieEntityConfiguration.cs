using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class MovieEntityConfiguration : EntityConfiguration<Movie>
    {
        public override void Configure(EntityTypeBuilder<Movie> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.SystemId)
                .HasColumnName("system_id")
                .IsRequired();

            builder.HasIndex(m => m.SystemId)
                .IsUnique();

            builder.Property(c => c.ImdbId)
                .HasColumnName("imdb_id");

            builder.Property(c => c.Title)
                .HasColumnName("title")
                .IsRequired();

            builder.Property(c => c.Overview)
                .HasColumnName("overview");

            builder.Property(c => c.Status)
                .HasColumnName("status");

            builder.Property(c => c.Downloaded)
                .HasColumnName("downloaded");

            builder.Property(c => c.HasFile)
                .HasColumnName("has_file");

            builder.Property(c => c.Year)
                .HasColumnName("year");

            builder.Property(c => c.Added)
                .HasColumnName("added");

            builder.Property(c => c.PlexUrl)
                .HasColumnName("plex_url");

            builder.Property(c => c.PosterPath)
                .HasColumnName("poster_path");

            builder.HasOne(c => c.Ratings)
                .WithOne()
                .HasForeignKey<ContentRating>(cr => cr.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(m => m.TmdbId)
                .HasColumnName("tmdb_id");

            builder.Property(m => m.Studio)
                .HasColumnName("studio");

            builder.Property(m => m.InCinemas)
                .HasColumnName("in_cinemas");

            builder.Property(m => m.PhysicalRelease)
                .HasColumnName("physical_release");

            builder.Property(m => m.YoutubeTrailerId)
                .HasColumnName("youtube_trailer_id");
        }
    }
}
