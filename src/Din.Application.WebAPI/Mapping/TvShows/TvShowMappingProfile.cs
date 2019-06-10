using AutoMapper.Configuration;
using Din.Application.WebAPI.Mapping.Content.Resolvers;
using Din.Application.WebAPI.Mapping.TvShows.Converters;
using Din.Application.WebAPI.Models.Request;
using Din.Application.WebAPI.Models.Response;
using Din.Domain.Clients.Sonarr.Requests;
using Din.Domain.Clients.Sonarr.Responses;

namespace Din.Application.WebAPI.Mapping.TvShows
{
    public class TvShowMappingProfile : MapperConfigurationExpression
    {
        public TvShowMappingProfile()
        {
            CreateMap<SonarrTvShow, TvShowResponse>()
                .ForMember(dest => dest.Id, 
                    opt => opt.MapFrom<ContentIdResolver<SonarrTvShow, TvShowResponse>>());

            CreateMap<TvShowRequest, SonarrTvShowRequest>()
                .ConvertUsing<ToSonarrTvShowRequestConverter>();
        }
    }
}
