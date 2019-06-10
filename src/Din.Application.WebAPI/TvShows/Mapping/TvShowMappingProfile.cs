using AutoMapper.Configuration;
using Din.Application.WebAPI.Content;
using Din.Application.WebAPI.TvShows.Mapping.Converters;
using Din.Application.WebAPI.TvShows.Mapping.Resolvers;
using Din.Application.WebAPI.TvShows.Requests;
using Din.Application.WebAPI.TvShows.Responses;
using Din.Domain.Clients.Sonarr.Requests;
using Din.Domain.Clients.Sonarr.Responses;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace Din.Application.WebAPI.TvShows.Mapping
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

            CreateMap<TvShow, TvShowSearchResponse>()
                .ForMember(dest => dest.TmdbId, opt => opt.MapFrom<TvShowSearchIdResolver>())
                .ForMember(dest => dest.TvdbId, opt => opt.MapFrom<TvShowSearchTvdbIdResolver>())
                .ForMember(dest => dest.Genres, opt => opt.MapFrom<TvShowSearchGenreResolver>());
        }
    }
}
