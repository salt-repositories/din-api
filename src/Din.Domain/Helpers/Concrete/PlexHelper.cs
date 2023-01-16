using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Plex.Interfaces;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Extensions;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Models.Entities;
using Microsoft.Extensions.Logging;

namespace Din.Domain.Helpers.Concrete
{
    public class PlexHelper : IPlexHelper
    {
        private readonly IPlexClient _client;
        private readonly IPlexConfig _config;
        private readonly ILogger<IPlexHelper> _logger;

        public PlexHelper(IPlexClient client, IPlexConfig config, ILogger<PlexHelper> logger)
        {
            _client = client;
            _config = config;
            _logger = logger;
        }

        public async Task CheckIsOnPlex(IContent content, CancellationToken cancellationToken)
        {
            const double similar = 0.6;
            
            if (!string.IsNullOrEmpty(content.PlexUrl))
            {
                return;
            }

            var response = await _client.SearchByTitle(content.Title.ToLower(), cancellationToken);
            var matches = response.MediaContainer.Hub
                .Single(x => x.Type == content.GetType().Name.ToLower())
                .Metadata
                .Where(x => (x.Title.CalculateSimilarity(content.Title) > similar ||
                             content.AlternativeTitles.Any(alt => alt.CalculateSimilarity(x.Title) > similar)) &&
                            x.Year.MoreOrLessThen(Convert.ToInt32(content.Year), 2))
                .ToList();

            if (matches.Any())
            {
                _logger.LogInformation("found matches: {Select}", matches.Select(x => $"{x.Title} ({x.Type})"));
                content.PlexUrl = $"https://app.plex.tv/desktop#!/server/{_config.ServerGuid}/details?key={matches.First().Key}";
            }
        }

        public async Task CheckIsOnPlex(IEnumerable<IContent> content, CancellationToken cancellationToken)
        {
            var collections = content.Split(2).ToList();

            var tasks = collections.Select(collection => Task.Run(async () =>
            {
                foreach (var item in collection)
                {
                    await CheckIsOnPlex(item, cancellationToken);
                }
            }, cancellationToken));

            await Task.WhenAll(tasks);
        }
    }
}