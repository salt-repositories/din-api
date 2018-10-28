using Din.Service.Dto.Jwt;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        #region injections

        private readonly ITokenService _service;

        #endregion injections

        #region constructors

        public TokenController(ITokenService service)
        {
            _service = service;
        }

        #endregion constructors

        #region endpoints

        /// <summary>
        /// Get token
        /// </summary>
        /// <param name="data">Client ID and Secret</param>
        /// <returns>Status response/ token</returns>
        [AllowAnonymous, HttpPost]
        public IActionResult GetToken([FromBody] TokenRequestDto data)
        {
            var response = _service.CreateToken(data);
            if (response.valid)
                return Ok(new {access_token = response.token});

            return BadRequest(new {message = "ClientID or Secret Invalid"});
        }

        #endregion endpoints
    }
}