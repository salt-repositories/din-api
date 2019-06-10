using System.Threading.Tasks;
using AutoMapper;
using Din.Application.WebAPI.Models.Request;
using Din.Application.WebAPI.Models.Response;
using Din.Application.WebAPI.Versioning;
using Din.Domain.Commands.Authentication;
using Din.Domain.Models.Dtos;
using MediatR;
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

        private readonly IMediator _bus;
        private readonly IMapper _mapper;

        #endregion fields

        #region constructors

        public AuthenticationController(IMediator bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        #endregion constructors

        #region endpoints

        /// <summary>
        /// Request JWT Token
        /// </summary>
        /// <param name="credentials">Token Credentials</param>
        /// <returns>Authentication response</returns>
        [AllowAnonymous, HttpPost("token")]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LoginAsync([FromBody] AuthRequest credentials)
        {
            var command = new GenerateTokenCommand(_mapper.Map<AuthenticationDto>(credentials));
            var result = await _bus.Send(command);

            return Ok(_mapper.Map<AuthenticationResponse>(result));
        }
       
        #endregion endpoints
    }
}