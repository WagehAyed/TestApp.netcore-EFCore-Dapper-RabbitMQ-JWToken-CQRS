using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Common
{
    public class JwtClaimTypes
    {
        public const string Permissions = "Permissions";
        public const string Roles = "Roles";
        public const string Applications = "Applications";
        public const string UPSId = "UPSId";
        public const string IdentityNumber = "IdentityNumber";
        public const string ApplicationsUUID = "ApplicationsUUID";
        public const string SectorIdClaimType = "SectorId";
        public const string DivisionIdClaimType = "DivisionId"; 
        public const string DepartmentIdClaimType = "DepartmentId";
        public const string EmployeeIdClaimType = "EmployeeId";
        public const string WorkScopeIdClaimType = "WorkScopeId";
        public const string BranchIdClaimType = "BranchId";
        public const string IdClaimType = "Id";
        public const string UUIDClaimType = "UUID";
    }
}
