using TestApp.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Application.Services.UserManagement
{
    public interface ICurrentUser
    {
        int Id { get; }
        Guid UUID { get; }
        string FullName { get; }
        string IdentityNumber { get; }
        int SectorId { get; }
        int? DepartmentId { get; }
        int? BranchId { get; }
        int? DivisionId { get; }
        int? EmployeeId { get; }
        //WorkScope WorkScopeId { get; }
        //Task<bool> IsInRole(Role role);
        //Task<bool> HasPermission(Permission permission, bool userActiveRole = false);
        //Task<UserRolesAndPermissions> GetRolesAndPermissions();

        IEnumerable<Permission> ActiveRoleAndFlatPermissions { get; }
        bool CanImpersonate();
        void Impersonate(ApplicationUser user);
        //Role ActiveRole { get; }
    }
}
