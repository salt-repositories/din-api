using System;

namespace Din.Application.WebAPI.Movies.Responses
{
    public class MovieHistoryResponse
    {
        public long Id { get; set; }
        public string SourceTitle { get; set; }
        public QualityResponse Quality { get; set; }
        public bool QualityCutoffNotMet { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DownloadId { get; set; }
        public string EventType { get; set; }
        public long MovieId { get; set; }
        public MovieResponse Movie { get; set; }
    }

    public class QualityResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string Resolution { get; set; }
        public string Modifier { get; set; }
    }
}