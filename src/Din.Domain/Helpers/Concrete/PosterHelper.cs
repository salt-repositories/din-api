using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Helpers.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations;
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

        public Task GetPosters<T>(ICollection<T> content, CancellationToken cancellationToken) where T : Content
        {
            var exceptions = new ConcurrentQueue<Exception>();

            Parallel.ForEach(content, async (item) =>
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
                        item.PosterPath = result.Results[0].PosterPath;
                    }
                    else
                    {
                        var result = await _client.SearchTvShowAsync(item.Title, 0, cancellationToken);
                        item.PosterPath = result.Results[0].PosterPath;
                    }
                }
                catch (Exception exception)
                {
                    exceptions.Enqueue(exception);
                }
            });

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }

            return Task.CompletedTask;
        }
    }
}
