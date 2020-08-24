using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Plex.Interfaces;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Extensions;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Domain.Helpers.Concrete
{
    public class PlexHelper : IPlexHelper
    {
        private readonly Container _container;
        private readonly IPlexClient _client;
        private readonly IPlexConfig _config;

        public PlexHelper(Container container, IPlexClient client, IPlexConfig config)
        {
            _container = container;
            _client = client;
            _config = config;
        }

        public async Task CheckIsOnPlex<T>(IEnumerable<T> content, CancellationToken cancellationToken) where T : IContent
        {
            var exceptions = new ConcurrentQueue<Exception>();
            var workLists = content.DivideByNumberOfThreads(4);
            var tasks = workLists.Select(list => Task.Run(async () =>
            {
                using (AsyncScopedLifestyle.BeginScope(_container))
                {
                    var repository = _container.GetInstance<IContentPollStatusRepository>();

                    foreach (var item in list)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(item.PlexUrl))
                            {
                                return;
                            }

                            var pollStatus = await repository.GetPollStatusByContentIdAsync(item.Id, cancellationToken);

                            if (pollStatus.PlexUrlPolled.AddDays(1) >= DateTime.Now)
                            {
                                return;
                            }

                            var response = await _client.SearchByTitle(item.Title.ToLower(), cancellationToken);

                            if (response.MediaContainer?.Metadata?.Length > 0 && response.MediaContainer.Metadata[0]
                                    .Title.CalculateSimilarity(item.Title) > 0.6)
                            {
                                item.PlexUrl =
                                    $"https://app.plex.tv/desktop#!/server/{_config.ServerGuid}/details?key={response.MediaContainer.Metadata[0].Key}";
                            }

                            pollStatus.PlexUrlPolled = DateTime.Now;
                        }
                        catch (Exception exception)
                        {
                            if (!(exception is HttpClientException))
                            {
                                exceptions.Enqueue(exception);
                            }
                        }
                    }

                    await repository.SaveAsync(cancellationToken);
                }
            }, cancellationToken));

            await Task.WhenAll(tasks);

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}