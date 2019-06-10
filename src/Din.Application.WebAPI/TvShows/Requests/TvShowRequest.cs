using System.Collections.Generic;

namespace Din.Application.WebAPI.TvShows.Requests
{
    public class TvShowRequest
    {
        public string TvdbId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public IEnumerable<int> SeasonNumbers { get; set; }
        public string PosterPath { get; set; }
    }
}
