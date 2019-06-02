using System.Reflection;
using Din.Domain.Authorization.Authorizers.Interfaces;
using Din.Domain.Authorization.Handlers.Concrete;
using Din.Domain.Authorization.Handlers.Interfaces;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public  static class MediatrRegistration
    {
        public static void RegisterMediatr(this Container container, Assembly[] assemblies)
        {
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);

            container.Collection.Register(typeof(IPipelineBehavior<,>), assemblies);
            container.Collection.Register(typeof(IRequestPreProcessor<>), assemblies);
           
            container.Register(typeof(IAuthorizationHandler<>), typeof(AuthorizationHandler<>));
            
            container.Collection.Register(typeof(IRequestAuthorizer<>), assemblies);
            container.Collection.Register(typeof(IValidator<>), assemblies);
        }
    }
}
