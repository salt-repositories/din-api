using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class SeasonEntityConfiguration : EntityConfiguration<Season>
    {
        public override void Configure(EntityTypeBuilder<Season> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.TvShowId)
                .HasColumnName("tvshow_id")
                .IsRequired();

            builder.Property(s => s.SeasonsNumber)
                .HasColumnName("season_number")
                .IsRequired();

            builder.Property(s => s.EpisodeCount)
                .HasColumnName("episode_count")
                .IsRequired();

            builder.Property(s => s.TotalEpisodeCount)
                .HasColumnName("total_episode_count")
                .IsRequired();
        }
    }
}
