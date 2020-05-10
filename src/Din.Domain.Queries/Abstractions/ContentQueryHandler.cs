using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Helpers.Interfaces;
using Din.Domain.Models.Querying;
using Din.Domain.Stores.Interfaces;

namespace Din.Domain.Queries.Abstractions
{
    public abstract class ContentQueryHandler<T> where T : Content
    {
        private readonly IPlexHelper _plexHelper;
        private readonly IPosterHelper _posterHelper;
        protected readonly IContentStore<T> Store;

        protected ContentQueryHandler
        (
            IPlexHelper plexHelper,
            IPosterHelper posterHelper,
            IContentStore<T> store
        )
        {
            _plexHelper = plexHelper;
            _posterHelper = posterHelper;
            Store = store;
        }

        protected async Task RetrieveOptionalData(ICollection<T> collection, ContentQueryParameters contentQueryParameters, CancellationToken cancellationToken)
        {
            if (contentQueryParameters == null)
            {
                return;
            }

            if (contentQueryParameters.Plex== true)
            {
                await _plexHelper.CheckIsOnPlex(collection, cancellationToken);
            }

            if (contentQueryParameters.Poster == true)
            {
                await _posterHelper.GetPosters(collection, cancellationToken);
            }

            Store.UpdateMultiple(collection);
        }
    }
}