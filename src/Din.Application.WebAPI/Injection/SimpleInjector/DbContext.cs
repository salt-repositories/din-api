using Din.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class DbContext
    {
        public static void RegisterDbContext(this Container container, IConfiguration configuration, IHostingEnvironment environment)
        {
            var connectionString = environment.IsDevelopment()
                ? configuration.GetConnectionString("DevContext")
                : configuration.GetConnectionString("DinContext");

            container.Register(() => new DinContext(connectionString), Lifestyle.Scoped);
        }
    }
}
