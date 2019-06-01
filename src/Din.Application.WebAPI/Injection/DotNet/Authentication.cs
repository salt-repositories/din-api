using System.Text;
using Din.Application.WebAPI.AuthorizationHandlers;
using Din.Application.WebAPI.AuthorizationHandlers.Requirements;
using Din.Application.WebAPI.Constants;
using Din.Domain.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Din.Application.WebAPI.Injection.DotNet
{
    public static class Authentication
    {
        public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });
            services.AddSingleton<IAuthorizationHandler, TokenRoleHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationRoles.USER,
                    policy => policy.Requirements.Add(new RoleRequirement(AccountRole.User.ToString())));
                options.AddPolicy(AuthorizationRoles.MODERATOR,
                    policy => policy.Requirements.Add(new RoleRequirement(AccountRole.Moderator.ToString())));
                options.AddPolicy(AuthorizationRoles.ADMIN,
                    policy => policy.Requirements.Add(new RoleRequirement(AccountRole.Admin.ToString())));
            });
        }
    }
}
