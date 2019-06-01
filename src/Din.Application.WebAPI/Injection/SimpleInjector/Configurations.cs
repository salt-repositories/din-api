using Din.Domain.Clients.Configurations.Concrete;
using Din.Domain.Clients.Configurations.Interfaces;
using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class Configurations
    {
        public static void RegisterConfigurations(this Container container, IConfiguration configuration)
        {
            var tokenConfig = configuration.GetSection("Jwt").Get<TokenConfig>();
            var giphyClientConfig = configuration.GetSection("Giphy").Get<GiphyClientConfig>();
            var ipStackClientConfig = configuration.GetSection("IpStack").Get<IpStackClientConfig>();
            var movieClientConfig = configuration.GetSection("MovieClient").Get<MovieClientConfig>();
            var tvShowClientConfig = configuration.GetSection("TvShowClient").Get<TvShowClientConfig>();
            var unsplashConfig = configuration.GetSection("Unsplash").Get<UnsplashClientConfig>();
            var tmdbClientConfig = configuration.GetSection("TMDB").Get<TMDBClientConfig>();

            container.RegisterInstance<ITokenConfig>(tokenConfig);
            container.RegisterInstance<IGiphyClientConfig>(giphyClientConfig);
            container.RegisterInstance<IIpStackClientConfig>(ipStackClientConfig);
            container.RegisterInstance<IMovieClientConfig>(movieClientConfig);
            container.RegisterInstance<ITvShowClientConfig>(tvShowClientConfig);
            container.RegisterInstance<IUnsplashClientConfig>(unsplashConfig);
            container.RegisterInstance<ITMDBClientConfig>(tmdbClientConfig);
        }
    }
}