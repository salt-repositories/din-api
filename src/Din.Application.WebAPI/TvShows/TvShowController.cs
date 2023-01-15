using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Din.Application.WebAPI.Controller;
using Din.Application.WebAPI.Controller.Versioning;
using Din.Application.WebAPI.Querying;
using Din.Application.WebAPI.TvShows.Requests;
using Din.Application.WebAPI.TvShows.Responses;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Commands.TvShows;
using Din.Domain.Models.Entities;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.Querying;
using Din.Domain.Queries.TvShows;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Din.Application.WebAPI.TvShows
{
    [ApiVersion(ApiVersions.V1)]
    [VersionedRoute("tvshows")]
    [ControllerName("TvShows")]
    public class TvShowController : ApiController
    {
        private readonly IMediator _bus;

        public TvShowController(IMediator bus)
        {
            _bus = bus;
        }

        #region endpoints

        /// <summary>
        /// Get all tvShows
        /// </summary>
        /// <returns>Collection of tvShows</returns>
        [HttpGet]
        [ProducesResponseType(typeof(QueryResponse<TvShowResponse>), 200)]
        public async Task<IActionResult> GetTvShows
        (
            [FromQuery] QueryParametersRequest queryParameters,
            [FromQuery] TvShowFilters filters
        )
        {
            var result = await _bus.Send(new GetTvShowsQuery(queryParameters, filters));
            var response = ToTvShowResponse(result);
            
            return Ok(response);
        }

        /// <summary>
        /// Get tv show by ID
        /// </summary>
        /// <param name="id">System ID</param>
        /// <returns>Single TvShow</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TvShowResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTvShowById
        (
            [FromRoute] string id
        )
        {
            var query = Guid.TryParse(id, out var guid)
                ? (IRequest<TvShow>) new GetTvShowByIdQuery(guid)
                : new GetTvShowBySystemIdQuery(Convert.ToInt32(id));

            var result = await _bus.Send(query);

            return Ok<TvShowResponse>(result);
        }

        /// <summary>
        /// Search the tv show database by query
        /// </summary>
        /// <param name="query">(part) title</param>
        /// <returns>Collection of tv shows fro the tv show database</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<TvShowSearchResponse>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SearchTvShowAsync([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest(new {message = "The search query can not be empty"});
            }

            var result = await _bus.Send(new GetTvShowFromTmdbQuery(query));
            return Ok(result.Select(tvShow => (TvShowSearchResponse) tvShow));
        }

        /// <summary>
        /// Add tv show to system
        /// </summary>
        /// <param name="tvShow">Tv show to add</param>
        /// <returns>Added tv show</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TvShowResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddTvShowAsync([FromBody] TvShowRequest tvShow)
        {
            var result = await _bus.Send(new AddTvShowCommand(tvShow));

            return Created<TvShowResponse>(result);
        }

        /// <summary>
        /// Get the tv show release calendar for a specific timespan
        /// </summary>
        /// <param name="from">From date</param>
        /// <param name="till">Till date</param>
        /// <returns>Tv Show release calendar</returns>
        [HttpGet("calendar")]
        [ProducesResponseType(typeof(IEnumerable<TvShowEpisodeResponse>), 200)]
        public async Task<IActionResult> GetCalendar([FromQuery] string from, [FromQuery] string till)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(till))
            {
                return BadRequest(new {message = "Both dates are needed for this query"});
            }

            var query = new GetTvShowCalendarQuery((DateTime.Parse(from), DateTime.Parse(till)));
            var result = await _bus.Send(query);

            return Ok(result.Select(episode => (TvShowEpisodeResponse) episode));
        }

        /// <summary>
        /// Get the current tv show queue
        /// </summary>
        /// <returns>Tv show queue</returns>
        [HttpGet("queue")]
        [ProducesResponseType(typeof(IEnumerable<SonarrQueue>), 200)]
        public async Task<IActionResult> GetQueue()
        {
            var query = new GetTvShowQueueQuery();
            var result = await _bus.Send(query);

            return Ok(result);
        }

        #endregion endpoints
        
        private static QueryResponse<TvShowResponse> ToTvShowResponse(QueryResult<TvShow> result) =>
            new(result.Items.Select(item => (TvShowResponse) item), result.TotalCount);
    }
}