using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Din.Application.WebAPI.Models.Response;
using Din.Application.WebAPI.Versioning;
using Din.Domain.Queries.Media;
using MediatR;
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

        private readonly IMediator _bus;
        private readonly IMapper _mapper;

        #endregion injections

        #region constructors

        public MediaController(IMediator bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        #endregion constructors

        #region endpoints
        
        [HttpGet("gif")]
        [ProducesResponseType(typeof(GifResponse), 200)]
        public async Task<IActionResult> GetGif([FromQuery] string tag)
        {
            var query = new GetGifQuery(tag);
            var result = await _bus.Send(query);

            return Ok(_mapper.Map<GifResponse>(result));
        }

        [HttpGet("backgrounds")]
        [ProducesResponseType(typeof(IEnumerable<BackgroundResponse>), 200)]
        public async Task<IActionResult> GetBackgrounds()
        {
            var query = new GetBackgroundsQuery();
            var result = await _bus.Send(query);

            return Ok(_mapper.Map<IEnumerable<BackgroundResponse>>(result));
        }

        #endregion endpoints
    }
}