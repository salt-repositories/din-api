using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Models.Entities;
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

        public async Task GetPoster(IContent content, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(content.PosterPath))
            {
                return;
            }

            if (content.GetType() == typeof(Movie))
            {
                var result = await _client.SearchMovieAsync(content.Title, 0, false,
                    Convert.ToInt32(content.Year),
                    null,
                    0,
                    cancellationToken);
                content.PosterPath = result.Results.Count > 0
                    ? result.Results[0].PosterPath
                    : null;
            }
            else
            {
                var result = await _client.SearchTvShowAsync(content.Title, 0, false, 0, cancellationToken);
                content.PosterPath = result.Results.Count > 0
                    ? result.Results[0].PosterPath
                    : null;
            }
        }

        public async Task GetPoster(IEnumerable<IContent> content, CancellationToken cancellationToken)
        {
            var getPosterTasks = content.Select(item => GetPoster(item, cancellationToken));

            await Task.WhenAll(getPosterTasks);
        }
    }
}