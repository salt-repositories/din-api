using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Application.WebAPI.Versioning;
using Din.Domain.Clients.ResponseObjects;
using Din.Domain.Generators.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Din.Application.WebAPI.Versioning.ApiVersions;

namespace Din.Application.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion(V1)]
    [VersionedRoute("media")]
    [ControllerName("Media")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class MediaController : ControllerBase
    {
        #region injections

        private readonly IMediaGenerator _generator;

        #endregion injections

        #region constructors

        public MediaController(IMediaGenerator generator)
        {
            _generator = generator;
        }

        #endregion constructors

        #region endpoints
        
        [HttpGet("gif")]
        [ProducesResponseType(typeof(GiphyItem), 200)]
        public async Task<IActionResult> GetGif([FromQuery] string query)
        {
            return Ok(await _generator.GenerateGif(query));
        }

        [HttpGet("backgrounds")]
        [ProducesResponseType(typeof(IEnumerable<UnsplashItem>), 200)]
        public async Task<IActionResult> GetBackgrounds()
        {
            return Ok(await _generator.GenerateBackgroundImages());
        }


        #endregion endpoints
    }
}