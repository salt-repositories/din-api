using Din.Domain.Stores.Concrete;
using Din.Domain.Stores.Interfaces;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class StoreRegistration
    {
        public static void RegisterStores(this Container container)
        {
            container.Register<IMediaStore, MediaStore>(Lifestyle.Singleton);
            container.Register<IPlexPosterStore, PlexPosterStore>(Lifestyle.Singleton);
        }
    }
}
