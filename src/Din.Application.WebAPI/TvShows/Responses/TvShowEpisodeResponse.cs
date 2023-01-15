using System;
using Din.Domain.Models.Entities;

namespace Din.Application.WebAPI.TvShows.Responses
{
    public record struct TvShowEpisodeResponse
    {
        public Guid Id { get; set; }
        public TvShowResponse TvShow { get; set; }
        public Guid TvShowId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get;set; }
        public string Title { get; set; }
        public DateTime AirDate { get; set; }
        public string Overview { get; set; }
        public bool HasFile { get; set; }
        public bool Monitored { get; set; }

        public static implicit operator TvShowEpisodeResponse(Episode episode) => new()
        {
            Id = episode.Id,
            TvShow = episode.TvShow,
            TvShowId = episode.TvShowId,
            SeasonNumber = episode.SeasonNumber,
            EpisodeNumber = episode.EpisodeNumber,
            Title = episode.Title,
            AirDate = episode.AirDate,
            Overview = episode.Overview,
            HasFile = episode.HasFile,
            Monitored = episode.Monitored
        };
    }
}
