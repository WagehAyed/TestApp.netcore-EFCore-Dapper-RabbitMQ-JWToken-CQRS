using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Application.Security.PolicyRequirments
{
    public class MustBeAssignedToApplicationRequirement : IAuthorizationRequirement
    {
        public MustBeAssignedToApplicationRequirement(Guid applicationUUID) =>
     ApplicationUUID = applicationUUID;

        public Guid ApplicationUUID { get; }
    }
}
