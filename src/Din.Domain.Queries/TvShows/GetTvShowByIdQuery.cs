using System;
using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using MediatR;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowByIdQuery : IActivatedRequest, IRequest<TvShow>
    {
        public Guid Id { get; }

        public GetTvShowByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
