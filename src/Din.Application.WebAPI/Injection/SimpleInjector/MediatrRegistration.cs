using System.Reflection;
using Din.Domain.Authorization.Authorizers.Concrete;
using Din.Domain.Authorization.Authorizers.Interfaces;
using Din.Domain.Authorization.Handlers.Concrete;
using Din.Domain.Authorization.Handlers.Interfaces;
using Din.Domain.Commands.Authentication;
using Din.Domain.Extensions;
using Din.Domain.Loggers;
using Din.Domain.Loggers.Concrete;
using Din.Domain.Middlewares.Mediatr;
using Din.Domain.Queries.Accounts;
using Din.Infrastructure.DataAccess.Mediatr.Concrete;
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
                    typeof(RequestPreProcessorBehavior<,>),
                    typeof(TransactionProcessorBehaviour<,>),
                    typeof(RequestPostProcessorBehavior<,>)
                }
            );

            container.Collection.Register(typeof(IRequestPreProcessor<>), 
                new []
                {
                    typeof(AuthorizationMiddleware<>),
                    typeof(FluentValidationMiddleware<>)
                }
            );

            container.Collection.Register(typeof(IRequestPostProcessor<,>), 
                new []
                {
                    typeof(AuthenticationLogger<,>)
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

            container.Register(typeof(IRequestHandler<>), new []
            {
                typeof(GetAccountQueryHandler).Assembly,
                typeof(GetAddedContentQueryHandler).Assembly,
                typeof(GenerateTokenCommandHandler).Assembly
            }, Lifestyle.Scoped);
            container.Register(typeof(IRequestHandler<,>), new []
            {
                typeof(GetAccountQueryHandler).Assembly,
                typeof(GetAddedContentQueryHandler).Assembly,
                typeof(GenerateTokenCommandHandler).Assembly
            }, Lifestyle.Scoped);
        }
    }
}
