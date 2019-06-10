using AutoMapper;
using Din.Application.WebAPI.Movies.Responses;
using TMDbLib.Objects.Search;

namespace Din.Application.WebAPI.Movies.Mapping.Resolvers
{
    public class MovieSearchIdResolver : IValueResolver<SearchMovie, MovieSearchResponse, int>
    {
        public int Resolve(SearchMovie source, MovieSearchResponse destination, int destMember, ResolutionContext context)
        {
            return source.Id;
        }
    }
}
