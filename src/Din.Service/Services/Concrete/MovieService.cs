using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Din.Data;
using Din.Data.Entities;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.RequestObjects.Abstractions;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Din.Service.Dto.Content;
using Din.Service.DTO.Content;
using Din.Service.Services.Abstractions;
using Din.Service.Services.Interfaces;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace Din.Service.Services.Concrete
{
    /// <inheritdoc cref="IMovieService" />
    public class MovieService : ContentService, IMovieService
    {
        private readonly IMovieClient _movieClient;
        private readonly string _tmdbKey;

        public MovieService(DinContext context, IMovieClient movieClient, ITMDBClientConfig config, IMapper mapper) :
            base(context, mapper)
        {
            _movieClient = movieClient;
            _tmdbKey = config.Key;
        }

        public async Task<T> GetAllMoviesAsync<T>(int pageSize, int page, string sortKey, string sortDirection)
        {
            return typeof(T) == typeof(IEnumerable<MovieDto>)
                ? Mapper.Map<T>(await _movieClient.GetCurrentMoviesAsync<IEnumerable<McMovie>>(pageSize, page, sortKey, sortDirection))
                : Mapper.Map<T>(await _movieClient.GetCurrentMoviesAsync<McMovieContainer>(pageSize, page, sortKey, sortDirection));
        }

        public async Task<MovieDto> GetMovieByIdAsync(int id)
        {
            return Mapper.Map<MovieDto>(await _movieClient.GetMovieByIdAsync(id));
        }

        public async Task<IEnumerable<SearchMovie>> SearchMovieAsync(string query)
        {
            return Mapper.Map<IEnumerable<SearchMovie>>(
                (await new TMDbClient(_tmdbKey).SearchMovieAsync(query)).Results);
        }

        public async Task<(bool success, SearchMovie movie)> AddMovieAsync(SearchMovie movie, int id)
        {
            var movieDate = Convert.ToDateTime(movie.ReleaseDate);
            var requestObj = new McRequest
            {
                Title = movie.Title,
                Year = movieDate.Year,
                QualityProfileId = 0,
                ProfileId = "6",
                TitleSlug = GenerateTitleSlug(movie.Title, movieDate),
                Monitored = true,
                TmdbId = movie.Id,
                Images = new List<ContentRequestImage>
                {
                    new ContentRequestImage
                    {
                        CoverType = "poster",
                        Url = movie.PosterPath
                    }
                },
                MovieOptions = new McRequestOptions
                {
                    SearchForMovie = true
                }
            };

            var result = await _movieClient.AddMovieAsync(requestObj);

            if (result.status)
            {
                await LogContentAdditionAsync(movie.Title, id, ContentType.Movie, movie.Id, result.systemId);

                return (true, movie);
            }

            return (false, movie);
        }

        public async Task<IEnumerable<CalendarItemDto>> GetMovieCalendarAsync(DateTime start, DateTime end)
        {
            return Mapper.Map<IEnumerable<CalendarItemDto>>(
                await _movieClient.GetCalendarAsync<McCalendar>(start, end));
        }

        public async Task<IEnumerable<QueueDto>> GetMovieQueueAsync()
        {
            return Mapper.Map<IEnumerable<QueueDto>>(await _movieClient.GetQueue<McQueueItem>());
        }
    }
}