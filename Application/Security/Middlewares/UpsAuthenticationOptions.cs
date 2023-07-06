using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Application.Security.Middlewares
{
    public class UpsAuthenticationOptions
    {
        public string AuthenticationScheme { get; set; } = JwtBearerDefaults.AuthenticationScheme;
        public string Authority { get; set; }
        public string Audience { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public Guid ApplicationUUID { get; set; }
        public JwtBearerEvents Events { get; set; }
    }
}
