using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Application.WebAPI.Requests;
using Din.Domain.Dtos;
using Din.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMDbLib.Objects.Search;

namespace Din.Application.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        #region injectons

        private readonly IMovieService _service;

        #endregion injections

        #region constructors

        public MovieController(IMovieService service)
        {
            _service = service;
        }

        #endregion constructors

        #region endpoints

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>Collection of movies</returns>
        [Authorize, HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MovieDto>), 200)]
        public async Task<IActionResult> GetAllMovies([FromQuery] int pageSize, [FromQuery] int page, [FromQuery] string sortKey, [FromQuery] string sortDirection)
        {
            return pageSize.Equals(0) 
                ? Ok(await _service.GetAllMoviesAsync<IEnumerable<MovieDto>>(pageSize, page, sortKey, sortDirection)) 
                : Ok(await _service.GetAllMoviesAsync<MovieContainerDto>(pageSize, page, sortKey, sortDirection));
        }

        /// <summary>
        /// Get movie by ID
        /// </summary>
        /// <param name="id">system ID</param>
        /// <returns>Single movie</returns>
        [Authorize, HttpGet("{id}")]
        [ProducesResponseType(typeof(MovieDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMovieById([FromRoute] int id)
        {
            return Ok(await _service.GetMovieByIdAsync(id));
        }

        /// <summary>
        /// Search moviedatabase for movie
        /// </summary>
        /// <param name="query">Searchquery</param>
        /// <returns>Collection of results</returns>
        [HttpGet("search"), Authorize]
        [ProducesResponseType(typeof(IEnumerable<SearchMovie>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SearchMovie([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query)) return BadRequest(new {message = "query can not be empty"});

            return Ok(await _service.SearchMovieAsync(query));
        }

        /// <summary>
        /// Add movie to system
        /// </summary>
        /// <param name="data">Movie to add</param>
        /// <returns>Status response</returns>
        [HttpPost, Authorize]
        [ProducesResponseType(typeof(SearchMovie), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddMovieAsync([FromBody] MovieRequest data)
        {
            var result = await _service.AddMovieAsync(data.Movie, data.AccountId);

            if (result.success)
            {
                return Created("Movie has been added", new { system_id = result.systemId });
            }

            return BadRequest(new {error = "Movie has been not added"});
        }

        #endregion endpoints
    }
}