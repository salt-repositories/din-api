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
        public DbSet<LoginAttempt> LoginAttempt { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AccountImageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AddedContentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LoginAttemptEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LoginLocationEntityConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql(_connectionString);
        }
    }
}