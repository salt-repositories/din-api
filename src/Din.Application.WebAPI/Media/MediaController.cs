using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Din.Application.WebAPI.Controller;
using Din.Application.WebAPI.Controller.Versioning;
using Din.Application.WebAPI.Media.Responses;
using Din.Domain.Queries.Media;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Application.WebAPI.Media
{
    [ApiVersion(ApiVersions.V1)]
    [VersionedRoute("media")]
    [ControllerName("Media")]
    [AllowAnonymous]
    public class MediaController : ApiController
    {
        private readonly IMediator _bus;

        public MediaController(IMediator bus)
        {
            _bus = bus;
        }

        #region endpoints
        
        [HttpGet("gif")]
        [ProducesResponseType(typeof(GifResponse), 200)]
        public async Task<IActionResult> GetGif([FromQuery] string tag)
        {
            var result = await _bus.Send(new GetGifQuery(tag));
            return Ok<GifResponse>(result);
        }

        [HttpGet("backgrounds")]
        [ProducesResponseType(typeof(IEnumerable<BackgroundResponse>), 200)]
        public async Task<IActionResult> GetBackgrounds()
        {
            var result = await _bus.Send(new GetBackgroundsQuery());
            return Ok(result.Select(item => (BackgroundResponse) item));
        }

        #endregion endpoints
    }
}