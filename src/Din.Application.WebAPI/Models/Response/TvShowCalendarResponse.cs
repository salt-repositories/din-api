using System;

namespace Din.Application.WebAPI.Models.Response
{
    public class TvShowCalendarResponse
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
    }
}