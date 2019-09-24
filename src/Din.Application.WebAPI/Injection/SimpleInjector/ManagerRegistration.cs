using Din.Domain.Managers.Concrete;
using Din.Domain.Managers.Interfaces;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class ManagerRegistration
    {
        public static void RegisterManagers(this Container container)
        {
            container.Register<ITokenManager, TokenManager>(Lifestyle.Scoped);
            container.Register<IEmailManager, EmailManager>(Lifestyle.Scoped);
        }
    }
}
