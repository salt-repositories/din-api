using System.Reflection;
using Din.Domain.Authorization.Authorizers.Interfaces;
using Din.Domain.Authorization.Handlers.Concrete;
using Din.Domain.Authorization.Handlers.Interfaces;
using Din.Domain.Commands.Authentication;
using Din.Domain.Extensions;
using Din.Domain.Logging.Handlers.Concrete;
using Din.Domain.Logging.Handlers.Interfaces;
using Din.Domain.Logging.Loggers.Interfaces;
using Din.Domain.Mediatr.Concrete;
using Din.Domain.Middlewares.Mediatr;
using Din.Domain.Queries.Accounts;
using Din.Domain.Stores.Concrete;
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
                    typeof(RequestPostProcessorBehavior<,>),
                }
            );

            container.Collection.Register(typeof(IRequestPreProcessor<>), 
                new []
                {
                    typeof(AuthorizationMiddleware<>),
                    typeof(FluentValidationMiddleware<>),
                    typeof(ContentStorePreRequestUpdater<>),
                }
            );

            container.Collection.Register(typeof(IRequestPostProcessor<,>), 
                new []
                {
                    typeof(LoggingMiddleware<,>),
                    typeof(ContentStorePostRequestUpdater<,>)
                }
            );

            container.Register(typeof(IAuthorizationHandler<>), typeof(AuthorizationHandler<>));
            container.Register(typeof(ILoggingHandler<,>), typeof(LoggingHandler<,>));


            container.Collection.Register(typeof(IRequestAuthorizer<>), assemblies.GetGenericInterfaceImplementationTypes(typeof(IRequestAuthorizer<>)));
            container.Collection.Register(typeof(IValidator<>), assemblies);
            container.Collection.Register(typeof(IRequestLogger<,>), assemblies.GetGenericInterfaceImplementationTypes(typeof(IRequestLogger<,>)));

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
