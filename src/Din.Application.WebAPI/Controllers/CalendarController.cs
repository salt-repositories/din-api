using System;
using System.Linq;
using System.Threading.Tasks;
using Din.Domain.Dtos;
using Din.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Application.WebAPI.Controllers
{
    [ControllerName("Calendars")]
    [Authorize]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/[controller]")]
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
                DateRange = new Tuple<DateTime, DateTime>(DateTime.Now, DateTime.Now.AddMonths(1))
            };

            return Ok(calendarDto);
        }

        #endregion endpoints
    }
}