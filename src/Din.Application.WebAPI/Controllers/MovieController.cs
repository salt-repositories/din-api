using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Din.Application.WebAPI.Models.Request;
using Din.Application.WebAPI.Models.Response;
using Din.Application.WebAPI.Versioning;
using Din.Domain.Clients.Radarr.Requests;
using Din.Domain.Clients.Radarr.Responses;
using Din.Domain.Commands.Movies;
using Din.Domain.Queries.Movies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMDbLib.Objects.Search;
using static Din.Application.WebAPI.Versioning.ApiVersions;

namespace Din.Application.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion(V1)]
    [VersionedRoute("movies")]
    [ControllerName("Movies")]
    [Produces("application/json")]
    [Authorize]
    public class MovieController : ControllerBase
    {
        #region injectons

        private readonly IMediator _bus;
        private readonly IMapper _mapper;

        #endregion injections

        #region constructors

        public MovieController(IMediator bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        #endregion constructors

        #region endpoints

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>Collection of movies</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MovieResponse>), 200)]
        public async Task<IActionResult> GetMovies([FromQuery] string title)
        {
            IRequest<IEnumerable<RadarrMovie>> query;
            IEnumerable<RadarrMovie> result;

            if (title == null)
            {
                query = new GetMoviesQuery();
                result = await _bus.Send(query);

                return Ok(_mapper.Map<IEnumerable<MovieResponse>>(result));
            }

            query = new GetMoviesByTitleQuery(title);
            result = await _bus.Send(query);

            return Ok(_mapper.Map<IEnumerable<MovieResponse>>(result));
        }

        /// <summary>
        /// Get movie by ID
        /// </summary>
        /// <param name="id">system ID</param>
        /// <returns>Single movie</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MovieResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMovieById([FromRoute] int id)
        {
            var query = new GetMovieByIdQuery(id);
            var result = await _bus.Send(query);

            return Ok(_mapper.Map<MovieResponse>(result));
        }


        /// <summary>
        /// Search the movie database by query
        /// </summary>
        /// <param name="query">(part) title</param>
        /// <returns>Collection of movies from the movie database</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<SearchMovie>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SearchMovieByQuery([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest(new {message = "The search query can not be empty"});
            }

            var requestQuery = new GetMovieFromTmdbQuery(query);
            var result = await _bus.Send(requestQuery);

            return Ok(result);
        }

        /// <summary>
        /// Add movie to system
        /// </summary>
        /// <param name="movie">Movie to add</param>
        /// <returns>Added movie</returns>
        [HttpPost]
        [ProducesResponseType(typeof(RadarrMovie), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddMovie([FromBody] MovieRequest movie)
        {
            var command = new AddMovieCommand(_mapper.Map<RadarrMovieRequest>(movie));
            var result = await _bus.Send(command);

            return Created("", _mapper.Map<MovieResponse>(result));
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

            return Ok(_mapper.Map<IEnumerable<MovieResponse>>(result));
        }

        /// <summary>
        /// Get the current movie queue
        /// </summary>
        /// <returns>movie queue</returns>
        [HttpGet("queue")]
        [ProducesResponseType(typeof(IEnumerable<RadarrQueue>), 200)]
        public async Task<IActionResult> GetQueue()
        {
            var query = new GetMovieQueueQuery();
            var result = await _bus.Send(query);

            return Ok(result);
        }

        #endregion endpoints
    }
}