using Din.Data;
using Din.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Din.Application.WebAPI.Injection.DotNet
{
    public static class DbContext
    {
        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DinContext>(options =>
                options.UseMySql(configuration["Database:ConString"], b => { b.MigrationsAssembly("Din.Data.Migrations"); })
            );
        }
    }
}
