using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Abstractions;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete
{
    public class AuthorizationCodeArchiver : BackgroundTask
    {
        private readonly Container _container;
        private const int MaximumLifetimeInDays = 3;

        public AuthorizationCodeArchiver(ILogger<AuthorizationCodeArchiver> logger, Container container) : base(nameof(AuthorizationCodeArchiver), logger)
        {
            _container = container;
        }

        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await using (AsyncScopedLifestyle.BeginScope(_container))
            {
                Logger.LogInformation("Begin authorization code archive run");

                var repository = _container.GetInstance<IAuthorizationCodeRepository>();
                var codes = (await repository.GetAll(cancellationToken)).ToList();

                if (!codes.Any())
                {
                    Progress = 100;
                    Logger.LogInformation("Nothing to archive");
                }

                var codesToArchive = codes.Where(code => DateTime.Now > code.Generated.AddDays(MaximumLifetimeInDays))
                    .ToList();

                foreach (var code in codesToArchive)
                {
                    code.Active = false;
                    Progress = Progress / codesToArchive.Count * 100;
                }

                await repository.SaveAsync(cancellationToken);


                Logger.LogInformation("Finished authorization code archive run");
            }
        }
    }
}