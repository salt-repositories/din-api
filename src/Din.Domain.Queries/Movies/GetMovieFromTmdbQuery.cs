using System.Collections.Generic;
using Din.Domain.Authorization.Requests;
using MediatR;
using TMDbLib.Objects.Search;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieFromTmdbQuery : IActivatedRequest, IRequest<IEnumerable<SearchMovie>>
    {
        public string Query { get; }

        public GetMovieFromTmdbQuery(string query)
        {
            Query = query;
        }
    }
}
