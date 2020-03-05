using Din.Domain.Helpers.Concrete;
using Din.Domain.Helpers.Interfaces;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class HelperRegistration
    {
        public static void RegisterHelpers(this Container container)
        {
            container.Register<ITokenHelper, TokenHelper>(Lifestyle.Scoped);
            container.Register<IEmailHelper, EmailHelper>(Lifestyle.Scoped);
            container.Register<IPlexHelper, PlexHelper>(Lifestyle.Scoped);
        }
    }
}
