using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AcmeCorpS.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AccessMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "X-API-Key";
        private const string ValidApiKey = "YOUR_API_KEY";

        public AccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            if (!httpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKey) || apiKey != ValidApiKey)
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("Invalid API key.");
                return;
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AccessMiddlewareExtensions
    {
        public static IApplicationBuilder UseAccessMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AccessMiddleware>();
        }
    }
}
