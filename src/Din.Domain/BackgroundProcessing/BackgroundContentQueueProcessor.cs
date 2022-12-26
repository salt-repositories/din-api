using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.Extensions;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Domain.BackgroundProcessing
{
    public class BackgroundContentQueueProcessor : BackgroundService
    {
        private readonly Container _container;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public BackgroundContentQueueProcessor(Container container, IHostApplicationLifetime applicationLifetime)
        {
            _container = container;
            _applicationLifetime = applicationLifetime;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken) => Task.Run(async () =>
        {
            _applicationLifetime.ApplicationStarted.WaitHandle.WaitOne(-1);
            
            var contentQueue = _container.GetInstance<ContentPollingQueue>();

            await using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var plexHelper = _container.GetInstance<IPlexHelper>();
                var posterHelper = _container.GetInstance<IPosterHelper>();

                while (!cancellationToken.IsCancellationRequested)
                {
                    var content = contentQueue.DequeueMultiple(100).ToList();
                    
                    if (!content.Any())
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    
                    try
                    {
                        var checkPlexTask = plexHelper.CheckIsOnPlex(content, cancellationToken);
                        var getPosterTask = posterHelper.GetPoster(content, cancellationToken);

                        await Task.WhenAll(checkPlexTask, getPosterTask);
                        await UpdateContent(_container, content, cancellationToken);
                    }
                    catch (Exception exception)
                    {
                        _container.GetInstance<ILogger<BackgroundContentQueueProcessor>>().LogError(exception,
                            "Uncaught exception within background content queue processor");
                    }
                }
            }
        }, cancellationToken);

        private static Task UpdateContent(
            Container container,
            IEnumerable<IContent> content,
            CancellationToken cancellationToken)
        {
            return container.WithRepository<IContentPollStatusRepository>(async repository =>
            {
                foreach (var item in content)
                {
                    repository.Update(item);
                
                    var status = await repository.GetPollStatusByContentIdAsync(item.Id, cancellationToken);
                    var now = DateTime.Now;
                    status.PlexUrlPolled = now;
                    status.PosterUrlPolled = now;
                }

                await repository.SaveAsync(cancellationToken);
            });
        }
    }
}