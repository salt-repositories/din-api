using Microsoft.AspNetCore.Authorization;

namespace Din.Application.WebAPI.AuthorizationHandlers.Requirements
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string Role { get; }

        public RoleRequirement(string role)
        {
            Role = role;
        }
    }
}
