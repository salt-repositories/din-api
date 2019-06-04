using System.Reflection;
using Din.Domain.Authorization.Authorizers.Concrete;
using Din.Domain.Authorization.Authorizers.Interfaces;
using Din.Domain.Authorization.Handlers.Concrete;
using Din.Domain.Authorization.Handlers.Interfaces;
using Din.Domain.Extensions;
using Din.Domain.Middlewares.Mediatr;
using Din.Domain.Queries.Accounts;
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

            container.Collection.Register(typeof(IPipelineBehavior<,>),
                new[]
                {
                    typeof(RequestPreProcessorBehavior<,>)
                }
            );

            container.Collection.Register(typeof(IRequestPreProcessor<>), 
                new []
                {
                    typeof(AuthorizationMiddleware<>),
                    typeof(FluentValidationMiddleware<>)
                }
            );
           
            container.Register(typeof(IAuthorizationHandler<>), typeof(AuthorizationHandler<>));

            var authorizers = new[]
            {
                typeof(IdentityAuthorizer<>).Assembly,
                typeof(RoleAuthorizer<>).Assembly
            }.GetGenericInterfaceImplementationTypes(typeof(IRequestAuthorizer<>));

            container.Collection.Register(typeof(IRequestAuthorizer<>), authorizers);

            container.Collection.Register(typeof(IValidator<>), assemblies);

            container.Register(typeof(IRequestHandler<>), new [] { typeof(GetAccountsQuery).Assembly }, Lifestyle.Scoped);
            container.Register(typeof(IRequestHandler<,>), new [] { typeof(GetAccountsQuery).Assembly }, Lifestyle.Scoped);
        }
    }
}
