using AutoMapper.Configuration;
using Din.Application.WebAPI.Content;
using Din.Application.WebAPI.Querying;
using Din.Application.WebAPI.TvShows.Mapping.Converters;
using Din.Application.WebAPI.TvShows.Mapping.Resolvers;
using Din.Application.WebAPI.TvShows.Requests;
using Din.Application.WebAPI.TvShows.Responses;
using Din.Domain.Clients.Sonarr.Requests;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Querying;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace Din.Application.WebAPI.TvShows.Mapping
{
    public class TvShowMappingProfile : MapperConfigurationExpression
    {
        public TvShowMappingProfile()
        {
            CreateMap<QueryParametersRequest, QueryParameters<SonarrTvShow>>()
                .ConvertUsing<ToQueryParametersConverter<SonarrTvShow>>();

            CreateMap<FiltersRequest, Filters>();

            CreateMap<QueryResult<SonarrTvShow>, QueryResponse<TvShowResponse>>();
            
            CreateMap<TvShowRequest, SonarrTvShowRequest>()
                .ConvertUsing<ToSonarrTvShowRequestConverter>();

            CreateMap<Season, SeasonResponse>();
            CreateMap<SeasonStatistics, SeasonStatisticsResponse>();

            CreateMap<SonarrTvShow, TvShowResponse>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom<ContentIdResolver<SonarrTvShow, TvShowResponse>>());

            CreateMap<TvShow, TvShowSearchResponse>()
                .ForMember(dest => dest.TmdbId, opt => opt.MapFrom<TvShowSearchIdResolver>())
                .ForMember(dest => dest.TvdbId, opt => opt.MapFrom<TvShowSearchTvdbIdResolver>())
                .ForMember(dest => dest.Genres, opt => opt.MapFrom<TvShowSearchGenreResolver>());

            CreateMap<SearchTvSeason, TvShowSearchSeason>();
        }
    }
}
