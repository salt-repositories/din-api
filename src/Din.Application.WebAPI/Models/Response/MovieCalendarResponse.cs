using System;

namespace Din.Application.WebAPI.Models.Response
{
    public class MovieCalendarResponse
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public bool HasFile { get; set; }
        public string Status { get; set; }
        public DateTime InCinemas { get; set; }
        public DateTime PhysicalRelease { get; set; }
        public bool Downloaded { get; set; }
    }
}