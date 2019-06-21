using Din.Infrastructure.DataAccess.Repositories.Concrete;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class RepositoryRegistration
    {
        public static void RegisterRepositories(this Container container)
        {
            container.Register<IAccountRepository, AccountRepository>(Lifestyle.Scoped);
            container.Register<IAddedContentRepository, AddedContentRepository>(Lifestyle.Scoped);
            container.Register<ILoginAttemptRepository, LoginAttemptRepository>(Lifestyle.Scoped);
            container.Register<IAuthorizationCodeRepository, AuthorizationCodeRepository>(Lifestyle.Scoped);
        }
    }
}
