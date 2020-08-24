using System;
using Din.Domain.Authorization.Requests;
using Din.Domain.Models.Entities;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieByIdQuery : IActivatedRequest, IRequest<Movie>
    {
        public Guid Id { get; }

        public GetMovieByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
