using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Helpers.Interfaces;
using TMDbLib.Client;

namespace Din.Domain.Helpers.Concrete
{
    public class PosterHelper : IPosterHelper
    {
        private readonly TMDbClient _client;

        public PosterHelper(ITmdbClientConfig config)
        {
            _client = new TMDbClient(config.Key);
        }

        public async Task GetPosters<T>(ICollection<T> content, CancellationToken cancellationToken) where T : Content
        {
            var exceptions = new ConcurrentQueue<Exception>();
            var tasks = content.Select(item => Task.Run(async () =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.PosterPath))
                    {
                        return;
                    }

                    if (item.GetType() == typeof(RadarrMovie))
                    {
                        var result = await _client.SearchMovieAsync(item.Title, 0, false, Convert.ToInt32(item.Year),
                            cancellationToken);
                        item.PosterPath = result.Results.Count > 0
                            ? result.Results[0].PosterPath
                            : null;
                    }
                    else
                    {
                        var result = await _client.SearchTvShowAsync(item.Title, 0, cancellationToken);
                        item.PosterPath = result.Results.Count > 0
                            ? result.Results[0].PosterPath
                            : null;
                    }
                }
                catch (Exception exception)
                {
                    if (!(exception is HttpClientException))
                    {
                        exceptions.Enqueue(exception);
                    }
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