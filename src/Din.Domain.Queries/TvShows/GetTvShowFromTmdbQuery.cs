using System.Collections.Generic;
using Din.Domain.Authorization.Requests;
using MediatR;
using TMDbLib.Objects.TvShows;

namespace Din.Domain.Queries.TvShows
{
    public class GetTvShowFromTmdbQuery : IActivatedRequest, IRequest<IEnumerable<TvShow>>
    {
        public string Query { get; }

        public GetTvShowFromTmdbQuery(string query)
        {
            Query = query;
        }
    }
}
