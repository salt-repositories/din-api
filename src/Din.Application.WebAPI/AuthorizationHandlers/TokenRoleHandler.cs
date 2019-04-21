using System.Linq;
using System.Threading.Tasks;
using Din.Application.WebAPI.AuthorizationHandlers.Requirements;
using Din.Application.WebAPI.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Din.Application.WebAPI.AuthorizationHandlers
{
    public class TokenRoleHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var identity = context.User.Identities.FirstOrDefault();

            if (identity == null)
            {
                return Task.CompletedTask;;
            }

            if (!identity.HasClaim(c => c.Type.Equals("Role") && c.Value.Equals(requirement.Role)) &&
                !identity.HasClaim(c => c.Type.Equals("Role") && c.Value.Equals(AuthorizationRoles.ADMIN)))
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
