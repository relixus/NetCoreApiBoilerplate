using Microsoft.AspNetCore.Identity;
using NetCoreApiBoilerplate.BLL.Areas.Auth.Models;
using NetCoreApiBoilerplate.Context;
using System.Security.Claims;

namespace NetCoreApiBoilerplate.Middlewares.CustomAuthorization
{
    public class ClaimsCheckingMiddleware
    {
        private readonly RequestDelegate _next;
       

        public ClaimsCheckingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserManager<ApiUser> userManager)
        {
            var _userManager = userManager;
            var endpoint = context.GetEndpoint();

            if (endpoint != null)
            {
                var requireClaimAttribute = endpoint.Metadata.GetMetadata<RequireClaimAttribute>();

                if (requireClaimAttribute != null)
                {
                    var username = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    // Check the user's claim against the database
                    var hasClaim = await UserHasClaimAsync(username!, requireClaimAttribute, userManager);

                    if (!hasClaim)
                    {
                        // User is not authorized, return a forbidden response
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync($"You dont have the proper authorization to access this function - " +
                            $"\n{ requireClaimAttribute.ClaimType }, { requireClaimAttribute.ClaimValue }");
                        return;
                    }
                }
            }

            // User has the necessary claim, proceed with the request
            await _next(context);
        }

        private async Task<bool> UserHasClaimAsync(string username, RequireClaimAttribute attr, UserManager<ApiUser> userManager)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user == null) return false;

            var claims = await userManager.GetClaimsAsync(user);

            return claims.Any(c => c.Value == attr.ClaimValue && c.Type == attr.ClaimType);
        }
    }
}
