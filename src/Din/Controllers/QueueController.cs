using System.Linq;
using System.Threading.Tasks;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        public async Task<IActionResult> GetQueue()
        {
            return Ok((await _movieService.GetMovieQueueAsync()).Concat(await _tvShowService.GetTvShowQueueAsync()));
        }

        #endregion endpointss
    }
}