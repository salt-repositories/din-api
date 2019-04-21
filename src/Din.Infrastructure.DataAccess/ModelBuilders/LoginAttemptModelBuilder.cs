using Din.Domain.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Din.Infrastructure.DataAccess.ModelBuilders
{
    public static class LoginAttemptModelBuilder
    {
        public static void BuildLoginAttemptModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginAttempt>().ToTable("LoginAttempts")
                .HasOne(la => la.Location);
        }
    }
}
