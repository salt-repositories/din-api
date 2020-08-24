using AutoMapper.Configuration;
using Din.Application.WebAPI.Content.Responses;
using Din.Application.WebAPI.Movies.Mapping.Converters;
using Din.Application.WebAPI.Movies.Mapping.Resolvers;
using Din.Application.WebAPI.Movies.Requests;
using Din.Application.WebAPI.Movies.Responses;
using Din.Application.WebAPI.Querying;
using Din.Domain.Clients.Radarr.Requests;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Models.Entities;
using Din.Domain.Queries.Querying;
using TMDbLib.Objects.Search;

namespace Din.Application.WebAPI.Movies.Mapping
{
    public class MovieMappingProfile : MapperConfigurationExpression
    {
        public MovieMappingProfile()
        {
            CreateMap<QueryResult<Movie>, QueryResponse<MovieResponse>>();
            
            CreateMap<MovieRequest, RadarrMovieRequest>()
                .ConvertUsing<ToRadarrMovieRequestConverter>();

            CreateMap<RadarrMovie, Movie>();
            CreateMap<RadarrMovie, MovieResponse>();
            CreateMap<Movie, MovieResponse>();

            CreateMap<SearchMovie, MovieSearchResponse>()
                .ForMember(dest => dest.TmdbId,
                    opt => opt.MapFrom<MovieSearchIdResolver>())
                .ForMember(dest => dest.Genres,
                    opt => opt.MapFrom<MovieSearchGenreResolver>());

            CreateMap<RadarrQueue, QueueResponse>();

            CreateMap<RadarrHistoryRecord, MovieHistoryResponse>()
                .ForMember(dest => dest.Quality,
                    opt => opt.MapFrom<MovieHistoryQualityResolver>());
        }
    }
}
