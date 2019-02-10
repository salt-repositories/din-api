using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Din.Domain.Models.Dtos;
using Din.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Application.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        #region injections

        private readonly IMovieService _movieService;
        private readonly ITvShowService _tvShowService;

        #endregion injections

        #region contructors

        public QueueController(IMovieService movieService, ITvShowService tvShowService)
        {
            _movieService = movieService;
            _tvShowService = tvShowService;
        }

        #endregion constructors

        #region endpoints

        /// <summary>
        /// Get system queue
        /// </summary>
        /// <returns>Collection of queue items</returns>
        [Authorize, HttpGet]
        [ProducesResponseType(typeof(IEnumerable<QueueDto>), 200)]
        public async Task<IActionResult> GetQueue()
        {
            return Ok((await _movieService.GetMovieQueueAsync()).Concat(await _tvShowService.GetTvShowQueueAsync()));
        }

        #endregion endpointss
    }
}