using Din.Domain.BackgroundServices.Concrete;
using Din.Domain.Generators.Concrete;
using Din.Domain.Generators.Interfaces;
using Din.Domain.Services.Concrete;
using Din.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Din.Application.WebAPI.Injection.DotNet
{
    public static class CustomServices
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<ITvShowService, TvShowService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddSingleton<IMediaGenerator, MediaGenerator>();
            //services.AddSingleton<IHostedService, ContentUpdateService>();
        }
    }
}
