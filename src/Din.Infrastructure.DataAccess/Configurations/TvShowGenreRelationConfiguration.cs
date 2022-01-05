using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class ContentGenreRelationConfiguration : EntityConfiguration<TvShowGenre>
    {
        public override void Configure(EntityTypeBuilder<TvShowGenre> builder)
        {
            base.Configure(builder);

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
