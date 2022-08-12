using System;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.Helpers.Interfaces;
using Din.Infrastructure.DataAccess;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Domain.BackgroundProcessing
{
    public class BackgroundContentQueueProcessor : BackgroundService
    {
        private readonly Container _container;

        public BackgroundContentQueueProcessor(Container container)
        {
            _container = container;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken) => Task.Run(async () =>
        {
            var contentQueue = _container.GetInstance<ContentPollingQueue>();

            await using (AsyncScopedLifestyle.BeginScope(_container))
            {
                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        if (!contentQueue.TryDequeue(out var content))
                        {
                            Thread.Sleep(1000);
                            continue;
                        }
            
                        var plexHelper = _container.GetInstance<IPlexHelper>();
                        var posterHelper = _container.GetInstance<IPosterHelper>();
            
                        await plexHelper.CheckIsOnPlex(content, cancellationToken);
                        await posterHelper.GetPoster(content, cancellationToken);

                        // context.Update(content);
                        // await context.SaveChangesAsync(cancellationToken);
                    }
                }
                catch (Exception exception)
                {
                    _container.GetInstance<ILogger<BackgroundContentQueueProcessor>>().LogError(exception, "Uncaught exception within background content queue processor");
                }
            }
        }, cancellationToken);
    }
}
