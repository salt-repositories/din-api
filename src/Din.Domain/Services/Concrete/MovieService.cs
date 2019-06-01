using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Din.Domain.Clients.Configurations.Interfaces;
using Din.Domain.Clients.Interfaces;
using Din.Domain.Clients.RequestObjects;
using Din.Domain.Clients.RequestObjects.Abstractions;
using Din.Domain.Clients.ResponseObjects;
using Din.Domain.Models.Dtos;
using Din.Domain.Models.Entities;
using Din.Domain.Services.Abstractions;
using Din.Domain.Services.Interfaces;
using Din.Infrastructure.DataAccess;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace Din.Domain.Services.Concrete
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

        public async Task<McMovie> AddMovieAsync(SearchMovie movie, Guid id)
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

            await LogContentAdditionAsync(movie.Title, id, ContentType.Movie, movie.Id, result.SystemId);

            return result;
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