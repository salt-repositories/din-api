using System.Net;

namespace Din.Application.WebAPI.HealthCheck
{
    public class ApiStatusResponse
    {
        public HttpStatusCode Status { get; }
        public string Version { get; }
        public string Environment { get; }

        public ApiStatusResponse(HttpStatusCode status, string version, string environment)
        {
            Status = status;
            Version = version;
            Environment = environment;
        }
    }
}
