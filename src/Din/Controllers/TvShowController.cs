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
        /// Get all tvShows
        /// </summary>
        /// <returns>Collection of tvShows</returns>
        [Authorize, HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TvShowDto>), 200)]
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
        [ProducesResponseType(typeof(TvShowDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTvShowById([FromRoute] int id)
        {
            return Ok(await _service.GetTvShowByIdAsync(id));
        }

        /// <summary>
        /// Search tvshowdatabase for tvShow
        /// </summary>
        /// <param name="query">Searchquery</param>
        /// <returns>Collection of results</returns>
        [HttpPost("search"), Authorize]
        [ProducesResponseType(typeof(IEnumerable<SearchTv>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SearchTvShowAsync([FromQuery] string query)
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
        [ProducesResponseType(typeof(SearchTv), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddTvShowAsync([FromBody] string tvShowData)
        {
            var tvShow = JsonConvert.DeserializeObject<SearchTv>(tvShowData);

            if (tvShow == null)
            {
                return BadRequest(new {error = "The body is empty"});
            }

            var result = await _service.AddTvShowAsync(tvShow, 1);

            if (result.success)
            {
                return Ok(result.tvShow);
            }

            return BadRequest(new {error = "TvShow has not been added"});
        }

        #endregion endpoints
    }
}