using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Radarr.Interfaces;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Clients.Sonarr.Interfaces;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Exceptions.Concrete;
using Din.Domain.Models.Entities;
using Din.Domain.Queries.Querying;
using Din.Infrastructure.DataAccess.Repositories.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Accounts
{
    public class GetAddedContentQueryHandler : IRequestHandler<GetAddedContentQuery, QueryResult<AddedContent>>
    {
        private readonly IAddedContentRepository _repository;
        private readonly IRadarrClient _radarrClient;
        private readonly ISonarrClient _sonarrClient;

        public GetAddedContentQueryHandler
        (
            IAddedContentRepository repository,
            IRadarrClient radarrClient,
            ISonarrClient sonarrClient
        )
        {
            _repository = repository;
            _radarrClient = radarrClient;
            _sonarrClient = sonarrClient;
        }

        public async Task<QueryResult<AddedContent>> Handle(GetAddedContentQuery request,
            CancellationToken cancellationToken)
        {
            var addedContent = await _repository.GetAddedContentByAccountId
            (
                request.Identity,
                request.QueryParameters,
                request.Filters,
                cancellationToken
            );
            var count = await _repository.Count(request.Identity, request.Filters, cancellationToken);

            var movieQueue = _radarrClient.GetQueueAsync(cancellationToken);
            var tvShowQueue = _sonarrClient.GetQueueAsync(cancellationToken);

            await Task.WhenAll(movieQueue, tvShowQueue);

            var tasks = addedContent.Where(ac => ac.Status != ContentStatus.Done)
                .Select(content => Task.Run(async () =>
                {
                    try
                    {
                        content.Status = await CheckStatus(content, await movieQueue, await tvShowQueue, cancellationToken);
                    }
                    catch (HttpClientException)
                    {
                        content.Status = ContentStatus.NotAvailable;
                    }

                    _repository.Update(content);
                }, cancellationToken))
                .ToList();

            await Task.WhenAll(tasks);

            return new QueryResult<AddedContent>(addedContent, count);
        }

        private async Task<ContentStatus> CheckStatus
        (
            AddedContent addedContent,
            IEnumerable<RadarrQueue> movieQueue,
            IEnumerable<SonarrQueue> tvShowQueue,
            CancellationToken cancellationToken
        )
        {
            if (addedContent.Type == ContentType.Movie)
            {
                var contentItem = await _radarrClient.GetMovieByIdAsync(addedContent.SystemId, cancellationToken);

                if (contentItem.Downloaded)
                {
                    return ContentStatus.Done;
                }

                if (movieQueue.FirstOrDefault(q => q.Id.Equals(addedContent.SystemId)) != null)
                {
                    return ContentStatus.Downloading;
                }


                var history = await _radarrClient.GetMovieHistoryAsync(addedContent.SystemId, cancellationToken);

                if (history.Records.Count <= 0)
                {
                    return ContentStatus.NotAvailable;
                }

                var lastHistoryRecord = history.Records.LastOrDefault(r => DateTimeOffset.Now.AddDays(-5) > r.Date);

                if (lastHistoryRecord != null)
                {
                    return ContentStatus.Stuck;
                }
            }
            else if (addedContent.Type == ContentType.TvShow)
            {
                if (tvShowQueue.FirstOrDefault(q => q.Id.Equals(addedContent.SystemId)) != null)
                {
                    return ContentStatus.Downloading;
                }

                var contentItem = await _sonarrClient.GetTvShowByIdAsync(addedContent.SystemId, cancellationToken);

                if (contentItem.TotalEpisodeCount == 0 || contentItem.FirstAired > DateTime.Now)
                {
                    return ContentStatus.NotAvailable;
                }

                if (contentItem.Downloaded || contentItem.EpisodeCount.Equals(contentItem.TotalEpisodeCount))
                {
                    return ContentStatus.Done;
                }

                if (contentItem.EpisodeCount < contentItem.TotalEpisodeCount)
                {
                    return ContentStatus.Downloading;
                }
            }

            return ContentStatus.Queued;
        }
    }
}