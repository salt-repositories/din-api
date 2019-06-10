using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Din.Application.WebAPI.Content;
using Din.Application.WebAPI.Movies.Responses;
using TMDbLib.Objects.Search;

namespace Din.Application.WebAPI.Movies.Mapping.Resolvers
{
    public class MovieSearchGenreResolver : IValueResolver<SearchMovie, MovieSearchResponse, IEnumerable<string>>
    {
        public IEnumerable<string> Resolve(SearchMovie source, MovieSearchResponse destination, IEnumerable<string> destMember,
            ResolutionContext context)
        {
            return source.GenreIds.Select(id => ((TmdbGenre) id).ToString());
        }
    }
}
