using System;
using System.Linq;
using System.Threading.Tasks;
using Din.Service.DTO.Content;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
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

        [Authorize, HttpGet]
        public async Task<IActionResult> GetReleaseCalendarAsync()
        {
            var calendarDto = new CalendarDto
            {
                Items = (await _movieService.GetMovieCalendarAsync()).Concat(
                    await _tvShowService.GetTvShowCalendarAsync()),
                DateRange = new Tuple<DateTime, DateTime>(DateTime.Now, DateTime.Now.AddMonths(1))
            };

            return Ok(calendarDto);
        }

        #endregion endpoints
    }
}