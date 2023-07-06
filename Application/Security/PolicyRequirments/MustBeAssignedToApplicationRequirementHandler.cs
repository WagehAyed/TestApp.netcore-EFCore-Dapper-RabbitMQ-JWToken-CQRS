
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Common;

namespace TestApp.Application.Security.PolicyRequirments
{
    public class MustBeAssignedToApplicationRequirementHandler : AuthorizationHandler<MustBeAssignedToApplicationRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MustBeAssignedToApplicationRequirement requirement)
        {
            var applicationsUUIDClaim = context.User.Claims.Where(x => x.Type == JwtClaimTypes.ApplicationsUUID).FirstOrDefault() ?? null;
            if (applicationsUUIDClaim != null && !string.IsNullOrEmpty(applicationsUUIDClaim.Value))
            {
                var applicationsUUID = JsonConvert.DeserializeObject<List<Guid>>(applicationsUUIDClaim.Value);
                bool isApplicationAssigned = applicationsUUID.Any(x => x == requirement.ApplicationUUID);
                if (isApplicationAssigned)
                {
                    context.Succeed(requirement);
                    return;
                }
            }
            context.Fail();
            return;
        }
    }
}