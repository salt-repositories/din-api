using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowBySystemIdQuery : IActivatedRequest, IRequest<TvShow>
    {
        public int Id { get; }

        public GetTvShowBySystemIdQuery(int id)
        {
            Id = id;
        }
    }
}
