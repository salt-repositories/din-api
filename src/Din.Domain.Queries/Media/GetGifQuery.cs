using Din.Domain.Clients.Giphy.Responses;
using MediatR;

namespace Din.Domain.Queries.Media
{
    public class GetGifQuery : IRequest<Giphy>
    {
        public string Tag { get; }

        public GetGifQuery(string tag)
        {
            Tag = tag;
        }
    }
}
