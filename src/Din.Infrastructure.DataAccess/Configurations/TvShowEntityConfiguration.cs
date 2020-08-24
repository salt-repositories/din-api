using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class TvShowEntityConfiguration : IEntityTypeConfiguration<TvShow>
    {
        public void Configure(EntityTypeBuilder<TvShow> builder)
        {
            builder.ToTable("tvshow");

            builder.HasKey(t => t.Id)
                .HasName("id");
           
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
                .HasForeignKey<ContentRating>(cr => cr.ContentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(t => t.TvdbId)
                .HasColumnName("tvdb_id");

            builder.Property(t => t.SeasonCount)
                .HasColumnName("season_count")
                .IsRequired();

            builder.Property(t => t.TotalEpisodeCount)
                .HasColumnName("total_episode_count")
                .IsRequired();

            builder.Property(t => t.EpisodeCount)
                .HasColumnName("episode_count")
                .IsRequired();

            builder.Property(t => t.Network)
                .HasColumnName("network");

            builder.Property(t => t.AirTime)
                .HasColumnName("air_time");

            builder.Property(t => t.FirstAired)
                .HasColumnName("first_aired");

            builder.HasMany(t => t.Seasons)
                .WithOne()
                .HasForeignKey(s => s.TvShowId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
