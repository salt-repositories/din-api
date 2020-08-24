using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Extensions;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Models.Entities;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using TMDbLib.Client;

namespace Din.Domain.Helpers.Concrete
{
    public class PosterHelper : IPosterHelper
    {
        private readonly Container _container;
        private readonly TMDbClient _client;

        public PosterHelper(Container container, ITmdbClientConfig config)
        {
            _container = container;
            _client = new TMDbClient(config.Key);
        }

        public async Task GetPosters<T>(IEnumerable<T> content, CancellationToken cancellationToken) where T : IContent
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
                            if (!string.IsNullOrEmpty(item.PosterPath))
                            {
                                return;
                            }

                            var pollStatus = await repository.GetPollStatusByContentIdAsync(item.Id, cancellationToken);

                            if (pollStatus.PosterUrlPolled.AddDays(1) >= DateTime.Now)
                            {
                                return;
                            }

                            if (item.GetType() == typeof(Movie))
                            {
                                var result = await _client.SearchMovieAsync(item.Title, 0, false,
                                    Convert.ToInt32(item.Year),
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

                            pollStatus.PosterUrlPolled = DateTime.Now;
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