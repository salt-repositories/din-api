using System;
using AutoMapper;
using Din.Application.WebAPI.TvShows.Responses;
using TMDbLib.Objects.TvShows;

namespace Din.Application.WebAPI.TvShows.Mapping.Resolvers
{
    public class TvShowSearchTvdbIdResolver : IValueResolver<TvShow, TvShowSearchResponse, int>
    {
        public int Resolve(TvShow source, TvShowSearchResponse destination, int destMember, ResolutionContext context)
        {
            return Convert.ToInt32(source.ExternalIds.TvdbId);
        }
    }
}
