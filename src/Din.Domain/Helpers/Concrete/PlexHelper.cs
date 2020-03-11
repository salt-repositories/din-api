using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Plex.Interfaces;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Extensions;
using Din.Domain.Helpers.Interfaces;

namespace Din.Domain.Helpers.Concrete
{
    public class PlexHelper : IPlexHelper
    {
        private readonly IPlexClient _client;
        private readonly IPlexConfig _config;

        public PlexHelper(IPlexClient client, IPlexConfig config)
        {
            _client = client;
            _config = config;
        }

        public Task CheckIsOnPlex<T>(ICollection<T> content, CancellationToken cancellationToken) where T : Content
        {
            var exceptions = new ConcurrentQueue<Exception>();

            Parallel.ForEach(content, async (item) =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.PlexUrl))
                    {
                        return;
                    }

                    var response = await _client.SearchByTitle(item.Title.ToLower(), cancellationToken);

                    if
                    (
                        response.MediaContainer?.Metadata?.Length > 0 &&
                        response.MediaContainer.Metadata[0].Title.CalculateSimilarity(item.Title) > 0.6
                    )
                    {
                        item.PlexUrl =
                            $"https://app.plex.tv/desktop#!/server/{_config.ServerGuid}/details?key={response.MediaContainer.Metadata[0].Key}";
                    }
                }
                catch (Exception exception)
                {
                    exceptions.Enqueue(exception);
                }
            });

            foreach (var exception in exceptions)
            {
                if (exception is HttpClientException)
                {
                    exceptions.TryDequeue(out _);
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }

            return Task.CompletedTask;
        }
    }
}