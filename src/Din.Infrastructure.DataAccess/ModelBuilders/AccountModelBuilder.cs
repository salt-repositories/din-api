using Din.Domain.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess.ModelBuilders
{
    public static class AccountModelBuilder
    {
        public static void BuildAccountModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Accounts")
                .HasIndex(a => a.Username)
                .IsUnique();
            modelBuilder.Entity<Account>()
                .HasMany(a => a.AddedContent)
                .WithOne(ac => ac.Account)
                .HasForeignKey(ac => ac.AccountRef);
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Image);
        }
    }
}
