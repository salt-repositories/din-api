using System.Collections.Generic;
using Din.Domain.Clients.Unsplash.Responses;
using MediatR;

namespace Din.Domain.Queries.Media
{
    public class GetBackgroundsQuery : IRequest<IEnumerable<UnsplashImage>>
    {
    }
}
