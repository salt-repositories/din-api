using System;
using System.Linq.Expressions;
using Din.Domain.Context;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess
{
    public class DinContext : DbContext
    {
        private readonly string _connectionString;
        private readonly IRequestContext _context;
        private Guid AccountId => _context.GetIdentity();

        public DinContext()
        {
            _connectionString = "Server=127.0.0.1;Port=5656;Database=dev_din-api;Uid=root;Pwd=root;";
        }
        
        public DinContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DinContext(string connectionString, IRequestContext context)
        {
            _connectionString = connectionString;
            _context = context;
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<AddedContent> AddedContent { get; set; }
        public DbSet<AccountAuthorizationCode> AuthorizationCode { get; set; }

        public DbSet<LoginAttempt> LoginAttempt { get; set; }
        public DbSet<LoginLocation> LoginLocation { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<TvShow> TvShow { get; set; }
        public DbSet<Episode> Episode { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<ContentPollStatus> ContentPollStatus { get; set; }

        internal Expression<Func<T, bool>> AccountFilter<T>() where T : IScopedEntity =>
            entity => entity.Account.Id.Equals(AccountId);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AccountImageEntityConfiguration(this));
            modelBuilder.ApplyConfiguration(new AddedContentEntityConfiguration(this));
            modelBuilder.ApplyConfiguration(new AccountAuthorizationCodeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LoginAttemptEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LoginLocationEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MovieEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TvShowEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SeasonEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ContentGenreRelationConfiguration());
            modelBuilder.ApplyConfiguration(new GenreEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ContentRatingEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EpisodeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ContentPollStatusEntityConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }
    }
}