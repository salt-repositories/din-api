using Din.Application.WebAPI.Context;
using Din.Domain.Authorization.Context;
using Din.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class ContextRegistration
    {
        public static void RegisterContexts(this Container container)
        {
            container.Register<IRequestContext, RequestContext>();
        }

        public static void RegisterDbContext(this Container container, IConfiguration configuration, IHostingEnvironment environment)
        {
            var connectionString = environment.IsDevelopment()
                ? configuration.GetConnectionString("DevContext")
                : configuration.GetConnectionString("DinContext");

            container.Register(() => new DinContext(connectionString), Lifestyle.Scoped);
        }
    }
}
