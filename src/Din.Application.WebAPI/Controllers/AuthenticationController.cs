using System.Threading.Tasks;
using AutoMapper;
using Din.Application.WebAPI.Models.Request;
using Din.Application.WebAPI.Models.Response;
using Din.Application.WebAPI.Versioning;
using Din.Domain.Authorization.Context;
using Din.Domain.Models.Dtos;
using Din.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Din.Application.WebAPI.Versioning.ApiVersions;

namespace Din.Application.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion(V1)]
    [VersionedRoute("authentication")]
    [ControllerName("Authentication")]
    [Produces("application/json")]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        #region fields

        private readonly IAuthService _service;
        private readonly IRequestContext _requestContext;
        private readonly IMapper _mapper;

        #endregion fields

        #region constructors

        public AuthenticationController(IAuthService service, IRequestContext requestContext, IMapper mapper)
        {
            _service = service;
            _requestContext = requestContext;
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
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LoginAsync([FromBody] AuthRequest credentials)
        {
            var result = await _service.LoginAsync(_mapper.Map<AuthRequestDto>(credentials), _requestContext.GetUserAgentAsString(), _requestContext.GetRequestIpAsString());

            return Ok(_mapper.Map<AuthenticationResponse>(result));
        }
       
        #endregion endpoints
    }
}