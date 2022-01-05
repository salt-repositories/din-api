using System.Linq;
using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations.Abstractions
{
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(string.Concat(typeof(T).Name.Select((x,i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower());

            builder.HasKey(a => a.Id)
                .HasName("id");
        }
    }
}