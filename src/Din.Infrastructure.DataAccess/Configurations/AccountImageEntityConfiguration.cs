using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Din.Infrastructure.DataAccess.Configurations
{
    public class AccountImageEntityConfiguration : ScopedEntityConfiguration<AccountImage>
    {
        public AccountImageEntityConfiguration(DinContext context) : base(context)
        {
        }
        
        public override void Configure(EntityTypeBuilder<AccountImage> builder)
        {
            base.Configure(builder);

            builder.Property(ai => ai.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(ai => ai.Data)
                .HasColumnName("data")
                .IsRequired();
        }
    }
}
