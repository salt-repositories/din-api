using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Din.Application.WebAPI.TvShows.Responses;
using Din.Domain.Models.Entities;

namespace Din.Application.WebAPI.TvShows.Mapping.Resolvers
{
    public class TvShowResponseGenreResolver : IValueResolver<TvShow, TvShowResponse, IEnumerable<string>>
    {
        public IEnumerable<string> Resolve(TvShow source, TvShowResponse destination, IEnumerable<string> destMember, ResolutionContext context)
        {
            return source.Genres.Select(g => g.Genre.Name);
        }
    }
}
