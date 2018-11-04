using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Dto.Content;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace Din.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/[controller]")]
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
        public async Task<IActionResult> GetAllMovies()
        {
            return Ok(await _service.GetAllMoviesAsync());
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
        /// <param name="movieData">Movie to add</param>
        /// <returns>Status response</returns>
        [HttpPost, Authorize]
        [ProducesResponseType(typeof(SearchMovie), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddMovieAsync([FromBody] string movieData)
        {
            var movie = JsonConvert.DeserializeObject<SearchMovie>(movieData);

            if (movie == null)
            {
                return BadRequest(new {error = "The body is empty"});
            }

            var result = await _service.AddMovieAsync(movie, 1);

            if (result.success)
            {
                return Ok(result.movie);
            }

            return BadRequest(new {error = "Movie has been not added"});
        }

        #endregion endpoints
    }
}