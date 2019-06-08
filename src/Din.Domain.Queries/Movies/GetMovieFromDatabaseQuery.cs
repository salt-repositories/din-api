using System.Collections.Generic;
using MediatR;
using TMDbLib.Objects.Search;

namespace Din.Domain.Queries.Movies
{
    public class GetMovieFromDatabaseQuery : IRequest<IEnumerable<SearchMovie>>
    {
        public string Query { get; }

        public GetMovieFromDatabaseQuery(string query)
        {
            Query = query;
        }
    }
}
