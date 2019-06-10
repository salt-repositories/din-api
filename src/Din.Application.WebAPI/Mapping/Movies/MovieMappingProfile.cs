using AutoMapper.Configuration;
using Din.Application.WebAPI.Mapping.Content.Resolvers;
using Din.Application.WebAPI.Mapping.Movies.Converters;
using Din.Application.WebAPI.Models.Request;
using Din.Application.WebAPI.Models.Response;
using Din.Domain.Clients.Radarr.Requests;
using Din.Domain.Clients.Radarr.Responses;

namespace Din.Application.WebAPI.Mapping.Movies
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
        }
    }
}
