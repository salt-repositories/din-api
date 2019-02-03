using Din.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess
{
    public class DinContext : DbContext
    {
        public DinContext(DbContextOptions<DinContext> options) : base(options)
        {
        }

        public DbSet<AccountEntity> Account { get; set; }
        public DbSet<AccountImageEntity> AccountImage { get; set; }
        public DbSet<AddedContentEntity> AddedContent { get; set; }
        public DbSet<LoginAttemptEntity> LoginAttempt { get; set; }
        public DbSet<LoginLocationEntity> LoginLocation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountEntity>().ToTable("Account")
                .HasIndex(a => a.Username)
                .IsUnique();
            modelBuilder.Entity<AccountEntity>()
                .HasMany(a => a.AddedContent)
                .WithOne(ac => ac.Account)
                .HasForeignKey(ac => ac.AccountRef);
            modelBuilder.Entity<AccountEntity>()
                .HasOne(a => a.Image);
            modelBuilder.Entity<AccountEntity>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<AccountImageEntity>().ToTable("AccountImage");
            modelBuilder.Entity<AccountImageEntity>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<AddedContentEntity>().ToTable("AddedContent");
            modelBuilder.Entity<AddedContentEntity>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<LoginAttemptEntity>().ToTable("LoginAttempt")
                .HasOne(la => la.Location);
            modelBuilder.Entity<LoginAttemptEntity>().Property(l => l.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<LoginLocationEntity>().ToTable("LoginLocation");
            modelBuilder.Entity<LoginLocationEntity>().Property(l => l.Id).ValueGeneratedOnAdd();
        }
    }
}