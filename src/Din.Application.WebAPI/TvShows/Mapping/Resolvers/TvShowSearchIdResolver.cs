using AutoMapper;
using Din.Application.WebAPI.TvShows.Responses;
using TMDbLib.Objects.TvShows;

namespace Din.Application.WebAPI.TvShows.Mapping.Resolvers
{
    public class TvShowSearchIdResolver : IValueResolver<TvShow, TvShowSearchResponse, int>
    {
        public int Resolve(TvShow source, TvShowSearchResponse destination, int destMember, ResolutionContext context)
        {
            return source.Id;
        }
    }
}
