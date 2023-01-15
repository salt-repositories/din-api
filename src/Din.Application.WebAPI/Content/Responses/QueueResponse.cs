using System;
using Din.Application.WebAPI.Movies.Responses;
using Din.Application.WebAPI.TvShows.Responses;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Clients.Sonarr.Responses;

namespace Din.Application.WebAPI.Content.Responses
{
    public record struct QueueResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Size { get; set; }
        public double SizeLeft { get; set; }
        public TimeSpan TimeLeft { get; set; }
        public DateTime Eta { get; set; }
        public string Status { get; set; }
        public string DownloadId { get; set; }
        public string Protocol { get; set; }
        public MovieResponse Movie { get; set; }
        public TvShowResponse TvShow { get; set; }

        public static implicit operator QueueResponse(RadarrQueue radarrQueue) => new()
        {
            Id = radarrQueue.Id,
            Title = radarrQueue.Title,
            Size = radarrQueue.Size,
            SizeLeft = radarrQueue.SizeLeft,
            TimeLeft = radarrQueue.TimeLeft,
            Eta = radarrQueue.Eta,
            Status = radarrQueue.DownloadId,
            DownloadId = radarrQueue.DownloadId,
            Protocol = radarrQueue.Protocol,
            Movie = radarrQueue.Movie
        };
        
        public static implicit operator QueueResponse(SonarrQueue sonarrQueue) => new()
        {
            Id = sonarrQueue.Id,
            Title = sonarrQueue.Title,
            Size = sonarrQueue.Size,
            SizeLeft = sonarrQueue.SizeLeft,
            TimeLeft = sonarrQueue.TimeLeft,
            Eta = sonarrQueue.Eta,
            Status = sonarrQueue.DownloadId,
            DownloadId = sonarrQueue.DownloadId,
            Protocol = sonarrQueue.Protocol,
            TvShow = sonarrQueue.TvShow
        };
    }
}