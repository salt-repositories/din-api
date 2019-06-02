using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Application.WebAPI.Models.RequestsModels;
using Din.Application.WebAPI.Versioning;
using Din.Domain.Models.Dtos;
using Din.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMDbLib.Objects.Search;
using static Din.Application.WebAPI.Versioning.ApiVersions;

namespace Din.Application.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion(V1)]
    [VersionedRoute("tvshows")]
    [ControllerName("TvShows")]
    [Produces("application/json")]
    [Authorize]
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
        [HttpGet]
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
        [HttpGet("{id}")]
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
        [HttpPost("search")]
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
        /// <param name="data">Tvshow to add</param>
        /// <returns>Status response</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SearchTv), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddTvShowAsync([FromBody] TvShowRequest data)
        {
            var result = await _service.AddTvShowAsync(data.TvShow, data.AccountId);

            return Created("TvShow created", result);
        }

        #endregion endpoints
    }
}