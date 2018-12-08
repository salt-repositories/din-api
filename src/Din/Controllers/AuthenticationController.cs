using System.Threading.Tasks;
using AutoMapper;
using Din.Requests;
using Din.Service.Dtos;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IMapper _mapper;

        #endregion injections

        #region constructors

        public AuthenticationController(IAuthService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
        public async Task<IActionResult> LoginAsync([FromBody] AuthRequest credentials)
        {
            var result = await _service.LoginAsync(_mapper.Map<AuthDto>(credentials));

            if (result.success)
            {
                return Ok(new{access_token = result.token});
            }

            return BadRequest(new {error = result.message});
        }
       
        #endregion endpoints
    }
}