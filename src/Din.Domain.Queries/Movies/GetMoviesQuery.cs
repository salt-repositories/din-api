using System.Collections.Generic;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Models.Dtos;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesQuery : IRequest<IEnumerable<RadarrMovie>>
    {
    }
}