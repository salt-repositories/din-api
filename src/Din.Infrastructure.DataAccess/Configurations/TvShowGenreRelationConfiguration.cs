using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class ContentGenreRelationConfiguration : IEntityTypeConfiguration<TvShowGenre>
    {
        public void Configure(EntityTypeBuilder<TvShowGenre> builder)
        {
            builder.ToTable("tvshow_genre");

            builder.HasKey(c => new { c.TvshowId, c.GenreId });

            builder.Property(c => c.TvshowId)
                .HasColumnName("tvshow_id")
                .IsRequired();

            builder.Property(c => c.GenreId)
                .HasColumnName("genre_id")
                .IsRequired();

            builder.HasOne(c => c.TvShow)
                .WithMany(c => c.Genres)
                .HasForeignKey(c => c.TvshowId);

            builder.HasOne(c => c.Genre)
                .WithMany(g => g.TvShowGenres)
                .HasForeignKey(c => c.GenreId);
        }
    }
}
