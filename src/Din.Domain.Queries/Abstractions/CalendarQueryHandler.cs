using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Stores.Interfaces;

namespace Din.Domain.Queries.Abstractions
{
    public abstract class CalendarQueryHandler<T> where T : Content
    {
        private readonly IPlexPosterStore _store;
        private readonly IPlexHelper _plexHelper;
        private readonly IPosterHelper _posterHelper;

        protected CalendarQueryHandler(IPlexPosterStore store, IPlexHelper plexHelper, IPosterHelper posterHelper)
        {
            _store = store;
            _plexHelper = plexHelper;
            _posterHelper = posterHelper;
        }

        protected Task RetrieveAdditionalData(ICollection<T> collection, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
            // foreach (var item in collection)
            // {
            //     var result = _store.GetByTitle(item.Title);
            //
            //     if (result == null)
            //     {
            //         continue;
            //     }
            //
            //     item.PlexUrl = result.PlexUrl;
            //     item.PosterPath = result.PosterPath;
            // }
            //
            // await _plexHelper.CheckIsOnPlex(collection, cancellationToken);
            // await _posterHelper.GetPosters(collection, cancellationToken);
            //
            // foreach (var item in collection)
            // {
            //     _store.AddOne(new PlexPosterDto
            //     (
            //         item.Title,
            //         item.PlexUrl,
            //         item.PosterPath
            //     ));
            // }
        }
    }
}
