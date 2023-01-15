using System.Net;

namespace Din.Application.WebAPI.HealthCheck
{
    public record struct ApiStatusResponse(HttpStatusCode Status, string Version, string Environment)
    {
        public HttpStatusCode Status { get; init; } = Status;
        public string Version { get; init; } = Version;
        public string Environment { get; init; } = Environment;
    }
}
