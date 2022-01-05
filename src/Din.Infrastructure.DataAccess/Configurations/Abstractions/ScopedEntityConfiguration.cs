using Din.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations.Abstractions
{
    public abstract class ScopedEntityConfiguration<T> : EntityConfiguration<T> where T : class, IScopedEntity
    {
        private readonly DinContext _context;

        protected ScopedEntityConfiguration(DinContext context)
        {
            _context = context;
        }

        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            
            builder.HasQueryFilter(_context.AccountFilter<T>());
            
            builder.Property(a => a.AccountId)
                .HasColumnName("account_id")
                .IsRequired();
        }
    }
}