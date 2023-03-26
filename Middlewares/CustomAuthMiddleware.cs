using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NetCoreApiBoilerplate.Context;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace NetCoreApiBoilerplate.Middlewares
{
    public class CustomAuthMiddleware 
    {
        private readonly RequestDelegate _next;
  
        public CustomAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserContext userContext)
        {
            var userId = context.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            var user = await userContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null && user.BlockedUntil > DateTime.Now)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync($"Access Denied: You are banned until { user.BlockedUntil?.ToString() }");
                return;
            }

            await _next(context);
        }
    }




}
