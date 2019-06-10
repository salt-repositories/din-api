using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Din.Application.WebAPI.TvShows.Responses;
using TMDbLib.Objects.TvShows;

namespace Din.Application.WebAPI.TvShows.Mapping.Resolvers
{
    public class TvShowSearchGenreResolver : IValueResolver<TvShow, TvShowSearchResponse, IEnumerable<string>>
    {
        public IEnumerable<string> Resolve(TvShow source, TvShowSearchResponse destination, IEnumerable<string> destMember, ResolutionContext context)
        {
            return source.Genres.Select(genre => genre.Name);
        }
    }
}
