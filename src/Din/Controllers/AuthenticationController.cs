using System.Threading.Tasks;
using Din.Service.Dto.Auth;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        public async Task<IActionResult> LoginAsync([FromBody] CredentialsDto credentials)
        {
            var result = await _service.LoginAsync(credentials);

            if (result.status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns>Status response</returns>
        [Authorize, HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            // Cancel token?
            return Ok();
        }

        #endregion endpoints
    }
}