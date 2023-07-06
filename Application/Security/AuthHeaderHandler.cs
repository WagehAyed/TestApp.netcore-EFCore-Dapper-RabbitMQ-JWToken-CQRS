using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
namespace TestApp.Application.Security
{
    public class AuthHeaderHandler :DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthHeaderHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString().Replace("Bearer", "").Replace("bearer", ""));
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
