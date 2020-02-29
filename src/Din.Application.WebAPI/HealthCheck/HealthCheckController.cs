using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Din.Application.WebAPI.Versioning;
using Din.Infrastructure.DataAccess.Connections.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Din.Application.WebAPI.HealthCheck
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiVersionNeutral]
    [Route("health_check")]
    [VersionedRoute("health_check")]
    public class HealthCheckController : ControllerBase
    {
        private readonly IHealthCheckConnection _connection;
        private readonly IHostEnvironment _env;

        private static readonly string AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public HealthCheckController(IHealthCheckConnection connection, IHostEnvironment env)
        {
            _connection = connection;
            _env = env;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ObjectResult> GetApiStatus()
        {
            var canConnectDatabase = await _connection.CheckContextHealth(new CancellationToken());
            var status = canConnectDatabase ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;

            var statusResponse = new ApiStatusResponse(status, AssemblyVersion, _env.EnvironmentName);

            return StatusCode((int)status, statusResponse);
        }
    }
}
