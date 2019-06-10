using AutoMapper.Configuration;
using Din.Application.WebAPI.Content;
using Din.Application.WebAPI.Movies.Mapping.Converters;
using Din.Application.WebAPI.Movies.Mapping.Resolvers;
using Din.Application.WebAPI.Movies.Requests;
using Din.Application.WebAPI.Movies.Responses;
using Din.Domain.Clients.Radarr.Requests;
using Din.Domain.Clients.Radarr.Responses;
using TMDbLib.Objects.Search;

namespace Din.Application.WebAPI.Movies.Mapping
{
    public class MovieMappingProfile : MapperConfigurationExpression
    {
        public MovieMappingProfile()
        {
            CreateMap<RadarrMovie, MovieResponse>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom<ContentIdResolver<RadarrMovie, MovieResponse>>());

            CreateMap<MovieRequest, RadarrMovieRequest>()
                .ConvertUsing<ToRadarrMovieRequestConverter>();

            CreateMap<SearchMovie, MovieSearchResponse>()
                .ForMember(dest => dest.TmdbId,
                    opt => opt.MapFrom<MovieSearchIdResolver>())
                .ForMember(dest => dest.Genres,
                    opt => opt.MapFrom<MovieSearchGenreResolver>());
        }
    }
}
