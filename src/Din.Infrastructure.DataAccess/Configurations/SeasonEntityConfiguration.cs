using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class SeasonEntityConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.ToTable("season");

            builder.HasKey(s => s.Id)
                .HasName("id");

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
