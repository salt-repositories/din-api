using System.Collections.Generic;
using MediatR;
using TMDbLib.Objects.Search;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowFromTmdbQuery : IRequest<IEnumerable<SearchTv>>
    {
        public string Query { get; }

        public GetTvShowFromTmdbQuery(string query)
        {
            Query = query;
        }
    }
}
