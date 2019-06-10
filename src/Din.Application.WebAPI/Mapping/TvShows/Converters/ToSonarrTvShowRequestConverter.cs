using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Din.Application.WebAPI.Models.Request;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Sonarr.Requests;

namespace Din.Application.WebAPI.Mapping.TvShows.Converters
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
                TitleSlug = GenerateTitleSlug(source.Title, source.Year),
                Seasons = seasons,
                QualityProfileId = 0,
                ProfileId = "6",
                Monitored =  true,
                Images = new List<ContentImage>
                {
                    new ContentImage
                    {
                        CoverType = "poster",
                        Url = source.PosterPath
                    }
                }
            };

        }
        private string GenerateTitleSlug(string title, int year)
        {
            return $"{title.ToLower().Replace(" ", "-")}-{year}";
        }     
    }
}
