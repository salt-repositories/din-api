using System.Threading;
using System.Threading.Tasks;
using Din.Domain.Clients.Giphy.Interfaces;
using Din.Domain.Clients.Giphy.Responses;
using MediatR;

namespace Din.Domain.Queries.Media
{
    public class GetGifQueryHandler : IRequestHandler<GetGifQuery, Giphy>
    {
        private readonly IGiphyClient _client;

        public GetGifQueryHandler(IGiphyClient client)
        {
            _client = client;
        }

        public async Task<Giphy> Handle(GetGifQuery request, CancellationToken cancellationToken)
        {
            return await _client.GetRandomGifByTagAsync(request.Tag, cancellationToken);
        }
    }
}
