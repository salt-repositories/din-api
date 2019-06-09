using System.Collections.Generic;
using MediatR;
using TMDbLib.Objects.Search;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieFromTmdbQuery : IRequest<IEnumerable<SearchMovie>>
    {
        public string Query { get; }

        public GetMovieFromTmdbQuery(string query)
        {
            Query = query;
        }
    }
}
