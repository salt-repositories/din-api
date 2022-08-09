using AutoMapper;
using Din.Application.WebAPI.Content.Responses;
using Din.Application.WebAPI.Querying;
using Din.Application.WebAPI.TvShows.Mapping.Converters;
using Din.Application.WebAPI.TvShows.Mapping.Resolvers;
using Din.Application.WebAPI.TvShows.Requests;
using Din.Application.WebAPI.TvShows.Responses;
using Din.Domain.Clients.Sonarr.Requests;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Models.Entities;
using Din.Domain.Queries.Querying;
using TMDbLib.Objects.Search;
using Season = Din.Domain.Clients.Sonarr.Responses.Season;
using TvShow = TMDbLib.Objects.TvShows.TvShow;

namespace Din.Application.WebAPI.TvShows.Mapping
{
    public class TvShowMappingProfile : MapperConfigurationExpression
    {
        public TvShowMappingProfile()
        {
            CreateMap<QueryResult<Domain.Models.Entities.TvShow>, QueryResponse<TvShowResponse>>();
            
            CreateMap<TvShowRequest, SonarrTvShowRequest>()
                .ConvertUsing<ToSonarrTvShowRequestConverter>();

            CreateMap<Domain.Models.Entities.Season, SeasonResponse>();

            CreateMap<TvShow, TvShowResponse>();
            CreateMap<SonarrTvShow, TvShowResponse>();
            CreateMap<Domain.Models.Entities.TvShow, TvShowResponse>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom<TvShowResponseGenreResolver>());

            CreateMap<TvShow, TvShowSearchResponse>()
                .ForMember(dest => dest.TmdbId, opt => opt.MapFrom<TvShowSearchIdResolver>())
                .ForMember(dest => dest.TvdbId, opt => opt.MapFrom<TvShowSearchTvdbIdResolver>())
                .ForMember(dest => dest.Genres, opt => opt.MapFrom<TvShowSearchGenreResolver>());

            CreateMap<SearchTvSeason, TvShowSearchSeason>();

            CreateMap<SonarrCalendar, TvShowCalendarResponse>();

            CreateMap<SonarrQueue, QueueResponse>();

            CreateMap<SonarrTvShow, Domain.Models.Entities.TvShow>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom<TvShowEntityGenreResolver>());
            CreateMap<Season, Domain.Models.Entities.Season>()
                .ConvertUsing<ToSeasonEntityConverter>();
            CreateMap<Season, SeasonResponse>();
            CreateMap<SonarrEpisode, Episode>()
                .ForMember(dest => dest.TvShow, opt => opt.MapFrom<EpisodeEntityTvShowResolver>());

            CreateMap<Episode, TvShowEpisodeResponse>();
        }
    }
}
