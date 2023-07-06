using TestApp.Application.Security.Models;
using Microsoft.AspNetCore.Http; 
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace TestApp.Application.Security.Middlewares
{
    public class ApplicationValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;
        private readonly Guid _applciationUUID;
        private readonly string _authority;
        private IdentityUserInfoModel _identityUserInfo;
        public ApplicationValidationMiddleware(RequestDelegate next, IDistributedCache cache, string authority, Guid applciationUUID, ILogger logger)
        {
            _next = next;
            _cache = cache;
            _applciationUUID = applciationUUID;
            _authority = authority;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var identityNumber = httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(identityNumber))
            {
                string authHeader = null;
                //SingalR check When to get the token from querystring
                if (httpContext.Request.Path.Value.Contains("/hubs/") && !string.IsNullOrEmpty(httpContext.Request.Query["access_token"]))
                {
                    authHeader = "Bearer " + httpContext.Request.Query["access_token"];
                }
                else
                {
                    authHeader = httpContext.Request.Headers["Authorization"];
                }
                if (!string.IsNullOrEmpty(authHeader))
                {
                    using var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Authorization", authHeader);
                    var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, _authority + "/connect/userinfo"));
                    if (result.IsSuccessStatusCode)
                    {
                        var responseMessage = await result.Content.ReadAsStringAsync();
                        _identityUserInfo = JsonConvert.DeserializeObject<IdentityUserInfoModel>(responseMessage);
                        if (_identityUserInfo.Applications.Any(x => x.UUID == _applciationUUID))
                        {
                            await _cache.SetStringAsync($"{identityNumber}:currentUser", responseMessage);
                            await _next(httpContext);
                            return;
                        }
                        await WriteUnauthorizedResponse(httpContext);
                        return;
                    }
                }
            }
            await _next(httpContext);
            return;
        }
        private async Task WriteUnauthorizedResponse(HttpContext httpContext)
        {
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await httpContext.Response.WriteAsync("Unauthorized");
        }
    }
}
