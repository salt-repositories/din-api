using System;
using Din.Domain.Clients.Abstractions;
using Din.Domain.Clients.Radarr.Responses;

namespace Din.Application.WebAPI.Movies.Responses
{
    public record struct MovieHistoryResponse
    {
        public long Id { get; init; }
        public string SourceTitle { get; init; }
        public QualityResponse Quality { get; init; }
        public bool QualityCutoffNotMet { get; init; }
        public DateTimeOffset Date { get; init; }
        public string DownloadId { get; init; }
        public string EventType { get; init; }
        public long MovieId { get; init; }
        public MovieResponse Movie { get; init; }

        public static implicit operator MovieHistoryResponse(RadarrHistoryRecord record) => new()
        {
            Id = record.Id,
            SourceTitle = record.SourceTitle,
            Quality = record.Quality,
            QualityCutoffNotMet = record.QualityCutoffNotMet,
            Date = record.Date,
            DownloadId = record.DownloadId,
            EventType = record.EventType,
            MovieId = record.MovieId,
            Movie = record.Movie
        };
    }

    public record struct QualityResponse
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Source { get; init; }
        public string Resolution { get; init; }
        public string Modifier { get; init; }

        public static implicit operator QualityResponse(RecordQuality qualityRecord) => new()
        {
            Id = qualityRecord.Quality.Id,
            Name = qualityRecord.Quality.Name,
            Source = qualityRecord.Quality.Source,
            Resolution = qualityRecord.Quality.Resolution,
            Modifier = qualityRecord.Quality.Modifier
        };
    }
}