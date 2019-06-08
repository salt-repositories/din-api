using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Unsplash.Interfaces;
using Din.Domain.Clients.Unsplash.Responses;
using Din.Domain.Stores.Interfaces;
using MediatR;

namespace Din.Domain.Queries.Media
{
    public class GetBackgroundsQueryHandler : IRequestHandler<GetBackgroundsQuery, IEnumerable<UnsplashImage>>
    {
        private readonly IUnsplashClient _client;
        private readonly IMediaStore _mediaStore;

        public GetBackgroundsQueryHandler(IUnsplashClient client, IMediaStore mediaStore)
        {
            _client = client;
            _mediaStore = mediaStore;
        }

        public async Task<IEnumerable<UnsplashImage>> Handle(GetBackgroundsQuery request,
            CancellationToken cancellationToken)
        {
            var backgrounds = _mediaStore.GetBackgrounds();

            if (backgrounds != null)
            {
                return backgrounds;
            }

            var newBackgrounds = (await _client.GetImagesAsync(cancellationToken)).ToList();
            _mediaStore.SetBackgrounds(newBackgrounds);

            return newBackgrounds;
        }
    }
}