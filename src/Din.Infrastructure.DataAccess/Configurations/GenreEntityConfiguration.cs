using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("genre");

            builder.HasKey(g => g.Id)
                .HasName("id");

            builder.Property(g => g.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.HasIndex(g => g.Name)
                .IsUnique();
        }
    }
}
