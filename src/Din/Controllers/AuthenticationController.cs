using System.Threading.Tasks;
using Din.Service.Dto.Auth;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;

namespace Din.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region injections

        private readonly IAuthService _service;

        #endregion injections

        #region constructors

        public AuthenticationController(IAuthService service)
        {
            _service = service;
        }

        #endregion constructors

        #region endpoints

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="credentials">Login Credentials</param>
        /// <returns>Status response</returns>
        [AllowAnonymous, HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LoginAsync([FromBody] CredentialsDto credentials)
        {
            var result = await _service.LoginAsync(credentials);

            if (result.success)
            {
                return Ok(new{access_token = result.token});
            }

            return BadRequest(new {error = result.message});
        }
       
        #endregion endpoints
    }
}