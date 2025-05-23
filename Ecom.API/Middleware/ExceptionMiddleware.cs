using Ecom.API.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace Ecom.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHostEnvironment environment;
        private readonly IMemoryCache memoryCache;
        private readonly TimeSpan _rateLimitWindow=TimeSpan.FromSeconds(30);

        public ExceptionMiddleware(RequestDelegate next,IHostEnvironment environment,IMemoryCache memoryCache)
        {
            this.next = next;
            this.environment = environment;
            this.memoryCache = memoryCache;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                ApplaySecurity(context);
                if (IsRequestAllowed(context) == false)
                {
                    context.Response.StatusCode=(int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";
                    var respones = new ApiException((int)HttpStatusCode.TooManyRequests, "Too Many requests . Please try again later");
                   await context.Response.WriteAsJsonAsync(respones);
                }
               await next(context);
            }
            catch (Exception ex)
            {

                context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = environment.IsDevelopment() ?
                    new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace) : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message);

                var json=JsonSerializer.Serialize(response);
               await context.Response.WriteAsync(json);
            }
        }
        private bool IsRequestAllowed(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();
            var cachKey = $"Rate:{ip}";
            var dateNow=DateTime.Now;
            var (timesTamp, count) = memoryCache.GetOrCreate(cachKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
                return (timesTamp: dateNow, count: 0);
            });
            if (dateNow - timesTamp < _rateLimitWindow)
            {
                if (count >= 100)
                {
                    return false;
                }
                memoryCache.Set(key: cachKey, (timesTamp, count + 1), _rateLimitWindow);
            }
            else
            {
                memoryCache.Set(key: cachKey, (timesTamp, count ), _rateLimitWindow);

            }
            return true;

        }
        private void ApplaySecurity(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-XSS-Protection"] = "1;mode=block";
            context.Response.Headers["X-Frame-Options"] = "DENY";
        }
    }
}
