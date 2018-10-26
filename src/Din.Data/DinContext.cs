using Din.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Din.Data
{
    public class DinContext : DbContext
    {
        public DinContext(DbContextOptions<DinContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<AccountEntity> Account { get; set; }
        public DbSet<AccountImageEntity> AccountImage { get; set; }
        public DbSet<AddedContentEntity> AddedContent { get; set; }
        public DbSet<LoginAttemptEntity> LoginAttempt { get; set; }
        public DbSet<LoginLocationEntity> LoginLocation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("User")
                .HasOne(u => u.Account)
                .WithOne(a => a.User)
                .HasForeignKey<AccountEntity>(a => a.UserRef);
            modelBuilder.Entity<AccountEntity>().ToTable("Account")
                .HasIndex(a => a.Username)
                .IsUnique();
            modelBuilder.Entity<AccountEntity>()
                .HasMany(a => a.AddedContent)
                .WithOne(ac => ac.Account)
                .HasForeignKey(ac => ac.AccountRef);
            modelBuilder.Entity<AccountEntity>()
                .HasOne(a => a.Image)
                .WithOne(ai => ai.Account)
                .HasForeignKey<AccountImageEntity>(ai => ai.AccountRef);
            modelBuilder.Entity<AccountImageEntity>().ToTable("AccountImage");
            modelBuilder.Entity<AddedContentEntity>().ToTable("AddedContent");
            modelBuilder.Entity<LoginAttemptEntity>().ToTable("LoginAttempt")
                .HasOne(la => la.Location);
            modelBuilder.Entity<LoginLocationEntity>().ToTable("LoginLocation");
        }
    }
}