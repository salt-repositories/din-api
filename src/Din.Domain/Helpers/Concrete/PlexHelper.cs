using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Plex.Interfaces;
using Din.Domain.Configurations.Interfaces;
using Din.Domain.Extensions;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Models.Entities;

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

        public async Task CheckIsOnPlex(IContent content, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(content.PlexUrl))
            {
                return;
            }

            var response = await _client.SearchByTitle(content.Title.ToLower(), cancellationToken);

            if (response.MediaContainer?.Metadata?.Length > 0 && response.MediaContainer.Metadata[0]
                    .Title.CalculateSimilarity(content.Title) > 0.6)
            {
                content.PlexUrl =
                    $"https://app.plex.tv/desktop#!/server/{_config.ServerGuid}/details?key={response.MediaContainer.Metadata[0].Key}";
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