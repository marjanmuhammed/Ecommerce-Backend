using Ecommerce_Backend.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Middlewares
{
    public class UserValidationMiddleware
    {
        private readonly RequestDelegate _next;
        public UserValidationMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext db)
        {
            // If endpoint allows anonymous, skip
            var endpoint = context.GetEndpoint();
            if (endpoint == null) { await _next(context); return; }

            var allowAnonymous = endpoint.Metadata.Any(m => m.GetType().Name == "AllowAnonymousAttribute");
            if (allowAnonymous) { await _next(context); return; }

            // If user not authenticated, let the Auth middleware handle it
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                await _next(context);
                return;
            }

            // get user id from token
            var idClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idClaim, out var userId))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Invalid token (no user id)" });
                return;
            }

            var user = await db.users.FindAsync(userId);
            if (user == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "User not found" });
                return;
            }

            if (user.IsBlocked)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { message = "User is blocked" });
                return;
            }

            // everything ok
            await _next(context);
        }
    }
}
