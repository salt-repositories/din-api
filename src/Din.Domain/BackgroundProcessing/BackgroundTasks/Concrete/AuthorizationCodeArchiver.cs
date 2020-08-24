using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Interfaces;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete
{
    public class AuthorizationCodeArchiver : IBackgroundTask
    {
        private readonly Container _container;
        private readonly ILogger<AuthorizationCodeArchiver> _logger;

        public AuthorizationCodeArchiver(Container container, ILogger<AuthorizationCodeArchiver> logger)
        {
            _container = container;
            _logger = logger;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Begin authorization code archive run");

            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var repository = _container.GetInstance<IAuthorizationCodeRepository>();

                var codes = (await repository.GetAll(cancellationToken)).ToList();

                if (codes.Count.Equals(0))
                {
                    _logger.LogInformation("Nothing to archive");

                    return;
                }

                foreach (var code in codes.Where(code => DateTime.Now > code.Generated.AddDays(3)))
                {
                    code.Active = false;
                }

                await repository.SaveAsync(cancellationToken);
            }

            _logger.LogInformation("Finished authorization code archive run");
        }
    }
}
