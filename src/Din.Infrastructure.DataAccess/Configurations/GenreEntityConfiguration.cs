using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class GenreEntityConfiguration : EntityConfiguration<Genre>
    {
        public override void Configure(EntityTypeBuilder<Genre> builder)
        {
            base.Configure(builder);

            builder.Property(g => g.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.HasIndex(g => g.Name)
                .IsUnique();
        }
    }
}
