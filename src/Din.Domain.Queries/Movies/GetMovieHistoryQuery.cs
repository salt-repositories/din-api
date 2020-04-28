using Din.Domain.Authorization.Requests;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Radarr.Responses;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieHistoryQuery : IActivatedRequest, IRequest<HistoryResult<RadarrHistoryRecord>>
    {
        public int Id { get; set; }

        public GetMovieHistoryQuery(int id)
        {
            Id = id;
        }
    }
}
