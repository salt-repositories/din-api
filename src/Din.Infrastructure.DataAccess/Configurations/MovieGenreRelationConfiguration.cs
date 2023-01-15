using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class MovieGenreRelationConfiguration : EntityConfiguration<MovieGenre>
    {
        public override void Configure(EntityTypeBuilder<MovieGenre> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.MovieId)
                .HasColumnName("movie_id")
                .IsRequired();

            builder.Property(c => c.GenreId)
                .HasColumnName("genre_id")
                .IsRequired();

            builder.HasOne(c => c.Movie)
                .WithMany(c => c.Genres)
                .HasForeignKey(c => c.MovieId);

            builder.HasOne(c => c.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(c => c.GenreId);
        }
    }
}
