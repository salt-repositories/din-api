using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class EpisodeEntityConfiguration : EntityConfiguration<Episode>
    {
        public override void Configure(EntityTypeBuilder<Episode> builder)
        {
            base.Configure(builder);
            
            builder.HasOne(e => e.TvShow)
                .WithMany(t => t.Episodes)
                .HasForeignKey(e => e.TvShowId);

            builder.Property(e => e.TvShowId)
                .HasColumnName("tvshow_id")
                .IsRequired();

            builder.Property(e => e.SeasonNumber)
                .HasColumnName("season_number")
                .IsRequired();

            builder.Property(e => e.EpisodeNumber)
                .HasColumnName("episode_number")
                .IsRequired();

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .IsRequired();

            builder.Property(e => e.AirDate)
                .HasColumnName("air_date")
                .IsRequired();

            builder.Property(e => e.Overview)
                .HasColumnName("overview");

            builder.Property(e => e.HasFile)
                .HasColumnName("has_file");

            builder.Property(e => e.Monitored)
                .HasColumnName("monitored");
        }
    }
}
