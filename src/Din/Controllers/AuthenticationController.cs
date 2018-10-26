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

        [Authorize, HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] CredentialsDto credentials)
        {
            var loginResult = await _service.LoginAsync(credentials);

            if (loginResult.status)
                return Ok(new {loginResult.message});

            return BadRequest(new {loginResult.message});
        }

        [Authorize, HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            // Cancel token?
            return Ok();
        }

        #endregion endpoints
    }
}