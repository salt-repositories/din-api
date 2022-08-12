using System.Collections.Generic;
using AutoMapper;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Models.Entities;

namespace Din.Application.WebAPI.TvShows.Mapping.Resolvers
{
    public class TvShowEntityGenreResolver : IValueResolver<SonarrTvShow, TvShow, ICollection<TvShowGenre>>
    {
        public ICollection<TvShowGenre> Resolve(SonarrTvShow source, TvShow destination, ICollection<TvShowGenre> destMember,
            ResolutionContext context)
        {
            return  new List<TvShowGenre>();
        }
    }
}