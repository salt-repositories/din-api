using System.Collections.Generic;
using System.Linq;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Sonarr.Requests;

namespace Din.Application.WebAPI.TvShows.Requests
{
    public record struct TvShowRequest
    {
        public string TvdbId { get; set; }
        public string Title { get; set; }
        public IEnumerable<int> SeasonNumbers { get; set; }
        public string PosterPath { get; set; }

        public static implicit operator SonarrTvShowRequest(TvShowRequest request) => new()
        {
            TvdbId = request.TvdbId,
            Title = request.Title,
            TitleSlug = $"{request.Title.ToLower().Replace(" ", "-").Replace(":", "")}",
            Seasons = request.SeasonNumbers
                .Select(number => new SonarrRequestSeason {Monitored = true, SeasonNumber = number}).ToList(),
            QualityProfileId = 6,
            Monitored = true,
            SeasonFolder = true,
            Images = new List<ContentImage>
            {
                new()
                {
                    CoverType = "poster",
                    Url = request.PosterPath
                }
            },
            AddOptions = new SonarrRequestAddOptions
            {
                IgnoreEpisodesWithFiles = true,
                IgnoreEpisodesWithoutFiles = false,
                SearchForMissingEpisodes = true
            },
            LanguageProfileId = 1
        };
    }
}