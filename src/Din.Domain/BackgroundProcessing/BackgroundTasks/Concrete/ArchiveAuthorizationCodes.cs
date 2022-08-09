using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Abstractions;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using SimpleInjector;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete
{
    public class ArchiveAuthorizationCodes : BackgroundTask
    {
        private const int MaximumLifetimeInDays = 3;

        public ArchiveAuthorizationCodes(Container container, ILogger<ArchiveAuthorizationCodes> logger) : base(
            container, logger, nameof(ArchiveAuthorizationCodes))
        {
        }

        protected override async Task OnExecuteAsync(Scope scope, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Begin authorization code archive run");

            var repository = scope.GetInstance<IAuthorizationCodeRepository>();
            var codes = (await repository
                .GetAll(cancellationToken))
                .Where(code => DateTime.Now > code.Generated.AddDays(MaximumLifetimeInDays) && code.Active)
                .ToList();

            if (!codes.Any())
            {
                CompleteProgress();
                Logger.LogInformation("Nothing to archive");
            }

            EnumerateAndDoWork(codes, code =>
            {
                code.Active = false;
            });

            await repository.SaveAsync(cancellationToken);

            Logger.LogInformation("Finished authorization code archive run");
        }
    }
}