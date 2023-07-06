using Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;
using Serilog.Context;
using System.Security.Claims;

namespace TestApp.Api.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private const string CorrelationIdHeaderKey = "X-Correlation-ID";
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext httpcontext)
        {
            string correlationId = null;

            if(httpcontext.Request.Headers.TryGetValue(CorrelationIdHeaderKey,out StringValues correlationIds))
            {
                correlationId = correlationIds.FirstOrDefault(x => x.Equals(CorrelationIdHeaderKey));
            }
            else
            {
                correlationId=Guid.NewGuid().ToString();
                httpcontext.Request.Headers.Add(CorrelationIdHeaderKey, correlationId);
            }

            httpcontext.Response.OnStarting(() =>
            {
                if(! httpcontext.Response.Headers.TryGetValue(CorrelationIdHeaderKey,out correlationIds))
                    httpcontext.Response.Headers.Add(CorrelationIdHeaderKey, correlationId);
                return Task.CompletedTask;
            });

            var claimsIdentity = httpcontext.User.Identity as ClaimsIdentity;
            using (LogContext.PushProperty("CorrelationId", correlationId))
            using (LogContext.PushProperty("UserName",httpcontext.User.Identity.IsAuthenticated? claimsIdentity.FindFirst (ClaimTypes.NameIdentifier) ?.Value: "<Anonymous>"))
            {
                await _next.Invoke(httpcontext);
            }
        }
    }
}
