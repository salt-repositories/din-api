using System;
using System.Linq;
using System.Threading.Tasks;
using Din.Application.WebAPI.Constants;
using Din.Application.WebAPI.Versioning;
using Din.Domain.Models.Dtos;
using Din.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Din.Application.WebAPI.Versioning.ApiVersions;

namespace Din.Application.WebAPI.Controllers
{
    [ApiVersion(V1)]
    [VersionedRoute("calendars")]
    [ControllerName("Calendars")]
    [Authorize(Policy = AuthorizationRoles.USER)]
    [Produces("application/json")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        #region injections

        private readonly IMovieService _movieService;
        private readonly ITvShowService _tvShowService;

        #endregion injections

        #region constructors

        public CalendarController(IMovieService movieService, ITvShowService tvShowService)
        {
            _movieService = movieService;
            _tvShowService = tvShowService;
        }

        #endregion constructors

        #region endpoints

        /// <summary>
        /// Get content release calendar
        /// </summary>
        /// <returns>Release calendar</returns>
        [HttpGet]
        [ProducesResponseType(typeof(CalendarItemDto), 200)]
        public async Task<IActionResult> GetReleaseCalendarAsync([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var calendarDto = new CalendarDto
            {
                Items = (await _movieService.GetMovieCalendarAsync(start, end)).Concat(
                    await _tvShowService.GetTvShowCalendarAsync(start, end)),
                DateRange = (start, end)
            };

            return Ok(calendarDto);
        }

        #endregion endpoints
    }
}