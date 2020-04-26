using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Din.Application.WebAPI.TvShows.Requests;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Sonarr.Requests;

namespace Din.Application.WebAPI.TvShows.Mapping.Converters
{
    public class ToSonarrTvShowRequestConverter : ITypeConverter<TvShowRequest, SonarrTvShowRequest>
    {
        public SonarrTvShowRequest Convert(TvShowRequest source, SonarrTvShowRequest destination, ResolutionContext context)
        {
            var seasons = source.SeasonNumbers.Select(number => new SonarrRequestSeason {Monitored = true, SeasonNumber = number}).ToList();

            return new SonarrTvShowRequest
            {
                TvdbId = source.TvdbId,
                Title =  source.Title,
                TitleSlug = GenerateTitleSlug(source.Title),
                Seasons = seasons,
                QualityProfileId = 0,
                ProfileId = "6",
                Monitored =  true,
                SeasonFolder = true,
                Images = new List<ContentImage>
                {
                    new ContentImage
                    {
                        CoverType = "poster",
                        Url = source.PosterPath
                    }
                },
                AddOptions = new SonarrRequestAddOptions
                {
                    IgnoreEpisodesWithFiles = true,
                    IgnoreEpisodesWithoutFiles = false,
                    SearchForMissingEpisodes = true
                }
            };

        }
        private string GenerateTitleSlug(string title)
        {
            return $"{title.ToLower().Replace(" ", "-").Replace(":", "")}";
        }     
    }
}
