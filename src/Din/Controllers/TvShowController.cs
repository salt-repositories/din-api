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
    public class TvShowController : ControllerBase
    {
        #region injections

        private readonly ITvShowService _service;

        #endregion injections

        #region constructors

        public TvShowController(ITvShowService service)
        {
            _service = service;
        }

        #endregion constructors

        #region endpoints

        /// <summary>
        /// Get all tvshows
        /// </summary>
        /// <returns>Collection of tvshows</returns>
        [Authorize, HttpGet]
        public async Task<IActionResult> GetAllTvShows()
        {
            return Ok(await _service.GetAllTvShowsAsync());
        }

        /// <summary>
        /// Get tvshow by system ID
        /// </summary>
        /// <param name="id">System ID</param>
        /// <returns>Single TvShow</returns>
        [Authorize, HttpGet("{id}")]
        public async Task<IActionResult> GetTvShowById([FromRoute] int id)
        {
            return Ok(await _service.GetTvShowByIdAsync(id));
        }

        /// <summary>
        /// Search tvshowdatabase for tvshow
        /// </summary>
        /// <param name="query">Searchquery</param>
        /// <returns>Collection of results</returns>
        [HttpPost("search"), Authorize]
        public async Task<IActionResult> SearchTvShowAsync(string query)
        {
            if (string.IsNullOrEmpty(query)) return BadRequest();

            return Ok(await _service.SearchTvShowAsync(query));
        }

        /// <summary>
        /// Add tvshow to system
        /// </summary>
        /// <param name="tvShowData">Tvshow to add</param>
        /// <returns>Status response</returns>
        [HttpPost, Authorize]
        public async Task<IActionResult> AddTvShowAsync(string tvShowData)
        {
            try
            {
                var tvShow = JsonConvert.DeserializeObject<SearchTv>(tvShowData);

                if (tvShow == null) return BadRequest();

                return Ok(await _service.AddTvShowAsync(tvShow, 1));
            }
            catch
            {
                return BadRequest();
            }
        }

        #endregion endpoints
    }
}