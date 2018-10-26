using System.Threading.Tasks;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace Din.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service)
        {
            _service = service;
        }

        [Authorize, HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            return Ok(await _service.GetAllMoviesAsync());
        }

        [Authorize, HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById([FromRoute] int id)
        {
            return Ok(await _service.GetMovieByIdAsync(id));
        }

        [HttpGet("search/{query}"), Authorize]
        public async Task<IActionResult> SearchMovie([FromRoute] string query)
        {
            if (string.IsNullOrEmpty(query)) return BadRequest(new {message = "query can not be empty"});

            return Ok(await _service.SearchMovieAsync(query));
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddMovieAsync(string movieData)
        {
            try
            {
                var movie = JsonConvert.DeserializeObject<SearchMovie>(movieData);

                if (movie == null) return BadRequest();

                return Ok(await _service.AddMovieAsync(movie, 1));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}