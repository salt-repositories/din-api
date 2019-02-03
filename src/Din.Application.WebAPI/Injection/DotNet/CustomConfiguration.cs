using Din.Domain.Config.Concrete;
using Din.Domain.Config.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Din.Application.WebAPI.Injection.DotNet
{
    public static class CustomConfiguration
    {
        public static void RegisterCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITokenConfig>(new TokenConfig(configuration["Jwt:Issuer"],
                configuration["Jwt:Key"]));
            services.AddSingleton<IGiphyClientConfig>(new GiphyClientConfig(configuration["GiphyClient:Url"],
                configuration["GiphyClient:Key"]));
            services.AddSingleton<IIpStackClientConfig>(new IpStackClientConfig(configuration["IpStackClient:Url"],
                configuration["IpStackClient:Key"]));
            services.AddSingleton<IMovieClientConfig>(new MovieClientConfig(configuration["MovieClient:Url"],
                configuration["MovieClient:Key"], configuration["MovieClient:SaveLocation"]));
            services.AddSingleton<ITvShowClientConfig>(new TvShowClientConfig(configuration["TvShowClient:Url"],
                configuration["TvShowClient:Key"], configuration["TvShowClient:SaveLocation"]));
            services.AddSingleton<IUnsplashClientConfig>(new UnsplashClientConfig(configuration["UnsplashClient:Url"],
                configuration["UnsplashClient:Key"]));
            services.AddSingleton<ITMDBClientConfig>(new TMDBClientConfig(configuration["TMDBClient:Key"]));
        }
    }
}
