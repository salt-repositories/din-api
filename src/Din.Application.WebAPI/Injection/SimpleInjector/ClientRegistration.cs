using Din.Domain.Clients.Giphy.Concrete;
using Din.Domain.Clients.Giphy.Interfaces;
using Din.Domain.Clients.IpStack.Concrete;
using Din.Domain.Clients.IpStack.Interfaces;
using Din.Domain.Clients.Plex.Concrete;
using Din.Domain.Clients.Plex.Interfaces;
using Din.Domain.Clients.Radarr.Concrete;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Sonarr.Concrete;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Unsplash.Concrete;
using Din.Domain.Clients.Unsplash.Interfaces;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class ClientRegistration
    {
        public static void RegisterClients(this Container container)
        {
            container.Register<IGiphyClient, GiphyClient>(Lifestyle.Scoped);
            container.Register<IIpStackClient, IpStackClient>(Lifestyle.Scoped);
            container.Register<IRadarrClient, RadarrClient>(Lifestyle.Scoped);
            container.Register<ISonarrClient, SonarrClient>(Lifestyle.Scoped);
            container.Register<IUnsplashClient, UnsplashClient>(Lifestyle.Scoped);
            container.Register<IPlexClient, PlexClient>(Lifestyle.Scoped);
        }
    }
}
