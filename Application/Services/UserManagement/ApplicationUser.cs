using TestApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Application.Services.UserManagement
{
    public class ApplicationUser
    {
        public int Id { get; internal set; }
        public Guid UUID { get; internal set; }
        public string UserName { get; internal set; }
        public string FullName { get; internal set; }
        public string IdentityNumber { get; internal set; }
        public string PhoneNumber { get; internal set; }
        public string Email { get; internal set; }
        public int SectorId { get; internal set; }
        public int? DepartmentId { get; internal set; }
        public int? BranchId { get; internal set; }
        public int? DivisionId { get; internal set; }
        public int? EmployeeId { get; internal set; }
        public int WorkScopeId { get; internal set; }
        public RecordStatus Status { get; internal set; }
    }
}
