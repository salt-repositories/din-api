using System.Collections.Generic;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Clients.Unsplash.Responses;

namespace Din.Domain.Stores.Interfaces
{
    public interface IMediaStore
    {
        IEnumerable<UnsplashImage> GetBackgrounds();
        void SetBackgrounds(IEnumerable<UnsplashImage> backgrounds);
        IEnumerable<RadarrMovie> GetMovies();
        RadarrMovie GetMovieById(int id);
        IEnumerable<RadarrMovie> GetMoviesByTitle(string title);
        void SetMovies(IEnumerable<RadarrMovie> movies);
        void AddMovie(RadarrMovie movie);
        void AddTvShow(SonarrTvShow tvShow);
    }
}
