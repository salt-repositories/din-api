using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess
{
    public class DinContext : DbContext
    {
        private readonly string _connectionString;

        public DinContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<AddedContent> AddedContent { get; set; }
        public DbSet<AccountAuthorizationCode> AuthorizationCode { get; set; }

        public DbSet<LoginAttempt> LoginAttempt { get; set; }
        public DbSet<LoginLocation> LoginLocation { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AccountImageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AddedContentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AccountAuthorizationCodeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LoginAttemptEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LoginLocationEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenEntityConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql(_connectionString);
        }
    }
}