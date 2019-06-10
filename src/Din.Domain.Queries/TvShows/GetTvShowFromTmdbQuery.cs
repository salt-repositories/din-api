using System.Collections.Generic;
using MediatR;
using TMDbLib.Objects.TvShows;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowFromTmdbQuery : IRequest<IEnumerable<TvShow>>
    {
        public string Query { get; }

        public GetTvShowFromTmdbQuery(string query)
        {
            Query = query;
        }
    }
}
