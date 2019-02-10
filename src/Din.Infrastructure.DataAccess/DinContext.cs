using Din.Domain.Models.Entity;
using Din.Infrastructure.DataAccess.ModelBuilders;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess
{
    public class DinContext : DbContext
    {
        public DinContext(DbContextOptions<DinContext> options) : base(options)
        {
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<AccountImage> AccountImage { get; set; }
        public DbSet<AddedContent> AddedContent { get; set; }
        public DbSet<LoginAttempt> LoginAttempt { get; set; }
        public DbSet<LoginLocation> LoginLocation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildAccountModel();
            modelBuilder.BuildLoginAttemptModel();
        }
    }
}