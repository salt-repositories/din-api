using System;
using Din.Domain.Clients.Sonarr.Responses;

namespace Din.Application.WebAPI.TvShows.Responses
{
    public record struct TvShowCalendarResponse
    {
        public int Id { get; set; }
        public int TvShowId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime AirDateUtc { get; set; }
        public DateTime AirDate { get; set; }
        public bool HasFile { get; set; }
        public bool Monitored { get; set; }
        public TvShowResponse TvShow { get; set; }

        public static implicit operator TvShowCalendarResponse(SonarrCalendar calendar) => new()
        {
            Id = calendar.Id,
            TvShowId = calendar.TvShowId,
            SeasonNumber = calendar.SeasonNumber,
            EpisodeNumber = calendar.EpisodeNumber,
            Title = calendar.Title,
            Overview = calendar.Overview,
            AirDateUtc = calendar.AirDateUtc,
            AirDate = calendar.AirDate,
            HasFile = calendar.HasFile,
            Monitored = calendar.Monitored,
            TvShow = (TvShowResponse) calendar.TvShow
        };
    }
}