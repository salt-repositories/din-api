using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Helpers.Interfaces;
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

        protected async Task RetrieveOptionalData(ICollection<T> collection, bool plex, bool poster,
            CancellationToken cancellationToken)
        {
            if (plex)
            {
                await _plexHelper.CheckIsOnPlex(collection, cancellationToken);
            }

            if (poster)
            {
                await _posterHelper.GetPosters(collection, cancellationToken);
            }

            Store.UpdateMultiple(collection);
        }
    }
}