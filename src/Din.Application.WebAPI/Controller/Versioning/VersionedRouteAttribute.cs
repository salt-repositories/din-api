using Microsoft.AspNetCore.Mvc;

namespace Din.Application.WebAPI.Controller.Versioning
{
    public class VersionedRouteAttribute : RouteAttribute
    {
        public VersionedRouteAttribute(string template) : base("v{version:apiVersion}/" + template.TrimStart('/'))
        {
        }
    }
}
