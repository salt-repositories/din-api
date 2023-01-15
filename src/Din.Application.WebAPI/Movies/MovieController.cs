using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Din.Application.WebAPI.Content.Responses;
using Din.Application.WebAPI.Controller;
using Din.Application.WebAPI.Controller.Versioning;
using Din.Application.WebAPI.Movies.Requests;
using Din.Application.WebAPI.Movies.Responses;
using Din.Application.WebAPI.Querying;
using Din.Domain.Commands.Movies;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Movies;
using Din.Domain.Queries.Querying;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Din.Application.WebAPI.Movies
{
    [ApiVersion(ApiVersions.V1)]
    [VersionedRoute("movies")]
    [ControllerName("Movies")]
    public class MovieController : ApiController
    {
        private readonly IMediator _bus;

        public MovieController(IMediator bus)
        {
            _bus = bus;
        }

        #region endpoints

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>Collection of movies</returns>
        [HttpGet]
        [ProducesResponseType(typeof(QueryResponse<MovieResponse>), 200)]
        public async Task<IActionResult> GetMovies
        (
            [FromQuery] QueryParametersRequest queryParameters,
            [FromQuery] MovieFilters filters
        )
        {
            var result = await _bus.Send(new GetMoviesQuery(queryParameters, filters));
            var response = ToMovieResponse(result);
            
            return Ok(response);
        }

        /// <summary>
        /// Get movie by ID
        /// </summary>
        /// <param name="id">system ID</param>
        /// <returns>Single movie</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MovieResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMovieById([FromRoute] string id)
        {
            var result = await _bus.Send(GetMovieRequest(id));
            return Ok<MovieResponse>(result);
        }

        /// <summary>
        /// Get movie history by ID
        /// </summary>
        /// <param name="id">system ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Collection of history</returns>
        [HttpGet("{id:int}/history")]
        [ProducesResponseType(typeof(IEnumerable<MovieHistoryResponse>), 200)]
        public async Task<IActionResult> GetMovieHistory([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetMovieHistoryQuery(id);
            var result = await _bus.Send(query, cancellationToken);

            return Ok(result.Select(record => (MovieHistoryResponse) record));
        }

        /// <summary>
        /// Search the movie database by query
        /// </summary>
        /// <param name="query">(part) title</param>
        /// <returns>Collection of movies from the movie database</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<MovieSearchResponse>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SearchMovieByQuery([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest(new {message = "The search query can not be empty"});
            }

            var result = await _bus.Send(new GetMovieFromTmdbQuery(query));
            return Ok(result.Select(searchMovie => (MovieSearchResponse) searchMovie));
        }

        /// <summary>
        /// Add movie to system
        /// </summary>
        /// <param name="movie">Movie to add</param>
        /// <returns>Added movie</returns>
        [HttpPost]
        [ProducesResponseType(typeof(MovieResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddMovie([FromBody] MovieRequest movie)
        {
            var result = await _bus.Send(new AddMovieCommand(movie));
            return Created<MovieResponse>(result);
        }

        /// <summary>
        /// Get the movies for a specific timespan
        /// </summary>
        /// <param name="from">From date</param>
        /// <param name="till">Till date</param>
        /// <returns>movie release calendar</returns>
        [HttpGet("calendar")]
        [ProducesResponseType(typeof(IEnumerable<MovieResponse>), 200)]
        public async Task<IActionResult> GetCalendar([FromQuery] string from, [FromQuery] string till)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(till))
            {
                return BadRequest(new {message = "Both dates are needed for this query"});
            }

            var query = new GetMovieCalendarQuery((DateTime.Parse(from), DateTime.Parse(till)));
            var result = await _bus.Send(query);

            return Ok(result.Select(movie => (MovieResponse) movie));
        }

        [HttpGet("queue")]
        [ProducesResponseType(typeof(IEnumerable<QueueResponse>), 200)]
        public async Task<IActionResult> GetQueue()
        {
            var result = await _bus.Send(new GetMovieQueueQuery());
            return Ok(result.Select(item => (QueueResponse) item));
        }

        #endregion endpoints
        
        private static QueryResponse<MovieResponse> ToMovieResponse(QueryResult<Movie> result) =>
            new(result.Items.Select(item => (MovieResponse) item), result.TotalCount);
        
        private static IRequest<Movie> GetMovieRequest(string id) => Guid.TryParse(id, out var guid)
            ? new GetMovieByIdQuery(guid)
            : new GetMovieBySystemIdQuery(Convert.ToInt32(id));
    }
}