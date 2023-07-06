using TestApp.Application.Security.Models;
using TestApp.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Application.Services.UserManagement
{
   public  class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributedCache _cache;

        private int? _id;
        private Guid? _uuid;
        private string _name;
        private string _fullName;
        private string _identityNumber;
        private int? _sectorId;
        private int? _divisionId;
        private int? _employeeId;
        private int? _departmentId;
        private int? _branchId;
        private string _UUID;
        //private WorkScope? _workScopeId;
        private readonly IdentityUserInfoModel _identityUserInfo;
        //private readonly UserRolesAndPermissions _userRolesAndPermissions;
        //public CurrentUser(IHttpContextAccessor httpContextAccessor,IDistributedCache cache)
        //{
        //    _httpContextAccessor=httpContextAccessor;
        //    _cache= cache;

        //   var identityNumber= _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? null;
        //    if(!string.IsNullOrEmpty(identityNumber))
        //    {
        //        var result=_cache.GetString($"{identityNumber}:currentUser");
        //        _identityUserInfo = JsonConvert.DeserializeObject<IdentityUserInfoModel>(result);
        //    }
        //}
        public string IdentityNumber => (_identityUserInfo .IdentityNumber);

        public int Id => throw new NotImplementedException();

        public Guid UUID => throw new NotImplementedException();

        public string FullName => throw new NotImplementedException();

        public int SectorId => throw new NotImplementedException();

        public int? DepartmentId => throw new NotImplementedException();

        public int? BranchId => throw new NotImplementedException();

        public int? DivisionId => throw new NotImplementedException();

        public int? EmployeeId => throw new NotImplementedException();

        public IEnumerable<Permission> ActiveRoleAndFlatPermissions
        {
            get
            {
                var permissions=new List<Permission>();
                return permissions;
            }
        }
        public bool CanImpersonate() => _httpContextAccessor.HttpContext?.User == null;

        public void Impersonate(ApplicationUser user)
        {
            if(! CanImpersonate()) 
                throw new InvalidOperationException("Impersonation is not valid in this context ");

            _id = user.Id;

        }
    }
}
