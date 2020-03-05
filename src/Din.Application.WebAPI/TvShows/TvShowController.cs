using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Din.Application.WebAPI.Querying;
using Din.Application.WebAPI.TvShows.Requests;
using Din.Application.WebAPI.TvShows.Responses;
using Din.Application.WebAPI.Versioning;
using Din.Domain.Clients.Sonarr.Requests;
using Din.Domain.Clients.Sonarr.Responses;
using Din.Domain.Commands.TvShows;
using Din.Domain.Models.Querying;
using Din.Domain.Queries.TvShows;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Application.WebAPI.TvShows
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [VersionedRoute("tvshows")]
    [ControllerName("TvShows")]
    [Produces("application/json")]
    [Authorize]
    public class TvShowController : ControllerBase
    {
        #region injections

        private readonly IMediator _bus;
        private readonly IMapper _mapper;

        #endregion injections

        #region constructors

        public TvShowController(IMediator bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        #endregion constructors

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
            [FromQuery] FiltersRequest filters,
            [FromQuery] bool plex
        )
        {
            var query = new GetTvShowsQuery(
                _mapper.Map<QueryParameters<SonarrTvShow>>(queryParameters),
                _mapper.Map<Filters>(filters),
                plex
            );
            var result = await _bus.Send(query);

            return Ok(_mapper.Map<QueryResponse<TvShowResponse>>(result));
        }

        /// <summary>
        /// Get tv show by ID
        /// </summary>
        /// <param name="id">System ID</param>
        /// <param name="plex">check is on plex</param>
        /// <returns>Single TvShow</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TvShowResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTvShowById([FromRoute] int id, [FromQuery] bool plex)
        {
            var query = new GetTvShowByIdQuery(id, plex);
            var result = await _bus.Send(query);

            return Ok(result);
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

            var requestQuery = new GetTvShowFromTmdbQuery(query);
            var result = await _bus.Send(requestQuery);

            return Ok(_mapper.Map<IEnumerable<TvShowSearchResponse>>(result));
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
            var command = new AddTvShowCommand(_mapper.Map<SonarrTvShowRequest>(tvShow));
            var result = await _bus.Send(command);

            return Created("", result);
        }

        /// <summary>
        /// Get the tv show release calendar for a specific timespan
        /// </summary>
        /// <param name="from">From date</param>
        /// <param name="till">Till date</param>
        /// <returns>Tv Show release calendar</returns>
        [HttpGet("calendar")]
        [ProducesResponseType(typeof(IEnumerable<TvShowCalendarResponse>), 200)]
        public async Task<IActionResult> GetCalendar([FromQuery] string from, [FromQuery] string till)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(till))
            {
                return BadRequest(new {message = "Both dates are needed for this query"});
            }

            var query = new GetTvShowCalendarQuery((DateTime.Parse(from), DateTime.Parse(till)));
            var result = await _bus.Send(query);

            return Ok(_mapper.Map<IEnumerable<TvShowCalendarResponse>>(result));
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
    }
}