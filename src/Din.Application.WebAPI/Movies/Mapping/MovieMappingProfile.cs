using AutoMapper.Configuration;
using Din.Application.WebAPI.Content;
using Din.Application.WebAPI.Movies.Mapping.Converters;
using Din.Application.WebAPI.Movies.Mapping.Resolvers;
using Din.Application.WebAPI.Movies.Requests;
using Din.Application.WebAPI.Movies.Responses;
using Din.Application.WebAPI.Querying;
using Din.Domain.Clients.Radarr.Requests;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Querying;
using TMDbLib.Objects.Search;

namespace Din.Application.WebAPI.Movies.Mapping
{
    public class MovieMappingProfile : MapperConfigurationExpression
    {
        public MovieMappingProfile()
        {
            CreateMap<QueryParametersRequest, QueryParameters<RadarrMovie>>()
                .ConvertUsing<ToQueryParametersConverter<RadarrMovie>>();

            CreateMap<FiltersRequest, Filters>();
            
            CreateMap<QueryResult<RadarrMovie>, QueryResponse<MovieResponse>>();
            
            CreateMap<MovieRequest, RadarrMovieRequest>()
                .ConvertUsing<ToRadarrMovieRequestConverter>();

            CreateMap<RadarrMovie, MovieResponse>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom<ContentIdResolver<RadarrMovie, MovieResponse>>());

            CreateMap<SearchMovie, MovieSearchResponse>()
                .ForMember(dest => dest.TmdbId,
                    opt => opt.MapFrom<MovieSearchIdResolver>())
                .ForMember(dest => dest.Genres,
                    opt => opt.MapFrom<MovieSearchGenreResolver>());

            CreateMap<RadarrQueue, MovieQueueResponse>();
        }
    }
}
