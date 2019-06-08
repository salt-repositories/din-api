using System;
using System.Collections.Generic;
using System.Linq;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Clients.Unsplash.Responses;
using Din.Domain.Extensions;
using Din.Domain.Stores.Interfaces;

namespace Din.Domain.Stores.Concrete
{
    public class MediaStore : IMediaStore
    {
        private (DateTime storeDate, IEnumerable<UnsplashImage> backgrounds) _backgroundData;
        private IEnumerable<RadarrMovie> _movies;

        public IEnumerable<UnsplashImage> GetBackgrounds()
        {
            var now = DateTime.Now;

            if (_backgroundData.storeDate.AddDays(1) <= now || _backgroundData.backgrounds == null)
            {
                return null;
            }
            
            return _backgroundData.backgrounds;
        }

        public void SetBackgrounds(IEnumerable<UnsplashImage> backgrounds)
        {
            _backgroundData.storeDate = DateTime.Now;
            _backgroundData.backgrounds = backgrounds;
        }

        public IEnumerable<RadarrMovie> GetMovies()
        {
            return _movies;
        }

        public RadarrMovie GetMovieById(int id)
        {
            return _movies?.FirstOrDefault(m => m.SystemId.Equals(id));
        }

        public IEnumerable<RadarrMovie> GetMoviesByTitle(string title)
        {
            return _movies?.Where(movie => title.CalculateSimilarity(movie.Title) > 0.4).ToList();
        }

        public void SetMovies(IEnumerable<RadarrMovie> movies)
        {
            _movies = movies;
        }

        public void AddMovie(RadarrMovie movie)
        {
            ((ICollection<RadarrMovie>) _movies)?.Add(movie);
        }

        public void AddTvShow(SonarrTvShow tvShow)
        {
            throw new NotImplementedException();
        }
    }
}
