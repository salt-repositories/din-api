using AutoMapper;
using Din.Domain.Clients.Sonarr.Responses;

namespace Din.Application.WebAPI.TvShows.Mapping.Converters
{
    public class ToSeasonEntityConverter : ITypeConverter<Season, Domain.Models.Entities.Season>
    {
        public Domain.Models.Entities.Season Convert(Season source, Domain.Models.Entities.Season destination, ResolutionContext context)
        {
            return new Domain.Models.Entities.Season
            {
                SeasonsNumber = source.SeasonsNumber,
                EpisodeCount = source.Statistics.EpisodeCount,
                TotalEpisodeCount = source.Statistics.TotalEpisodeCount
            };
        }
    }
}
