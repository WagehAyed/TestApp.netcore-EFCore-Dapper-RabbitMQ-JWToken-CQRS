using IdentityModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Application.Security.Models
{
    public class IdentityUserInfoModel
    {
        [JsonProperty(PropertyName = JwtClaimTypes.Name)]
        public string FullName { get; set; }
        [JsonProperty(PropertyName = JwtClaimTypes.Subject)]
        public string IdentityNumber { get; set; }
        
        public IEnumerable<ApplicationModel> Applications { get; set; }
    }
}
