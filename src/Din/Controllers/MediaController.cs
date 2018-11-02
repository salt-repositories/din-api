using System.Threading.Tasks;
using Din.Service.Generators.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
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
        
        [Authorize, HttpGet("gif")]
        public async Task<IActionResult> GetGif([FromQuery] string query)
        {
            return Ok(await _generator.GenerateGif(query));
        }

        [Authorize, HttpGet("backgrounds")]
        public async Task<IActionResult> GetBackgrounds()
        {
            return Ok(await _generator.GenerateBackgroundImages());
        }


        #endregion endpoints
    }
}