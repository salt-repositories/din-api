using System.Collections.Generic;
using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Radarr.Responses;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieHistoryQuery : IActivatedRequest, IRequest<IEnumerable<RadarrHistoryRecord>>
    {
        public int Id { get; }

        public GetMovieHistoryQuery(int id)
        {
            Id = id;
        }
    }
}
