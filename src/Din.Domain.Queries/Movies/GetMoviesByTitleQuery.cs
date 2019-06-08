using System.Collections.Generic;
using Din.Domain.Clients.Radarr.Responses;
using MediatR;

namespace Din.Domain.Queries.Movies
{
    public class GetMoviesByTitleQuery : IRequest<IEnumerable<RadarrMovie>>
    {
        public string Title { get; }

        public GetMoviesByTitleQuery(string title)
        {
            Title = title;
        }
    }
}
