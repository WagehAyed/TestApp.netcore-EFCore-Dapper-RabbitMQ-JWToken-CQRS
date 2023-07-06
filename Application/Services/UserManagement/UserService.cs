using TestApp.Application.Queries.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Application.Services.UserManagement
{
    internal class UserService : IUserService
    {
        private readonly IUserQueries _userQueries;
        private readonly ICurrentUser _currentUser;
        public UserService(IUserQueries userQueries, ICurrentUser currentUser)
        {
            _userQueries=userQueries;
            _currentUser = currentUser;

        }
        public async Task<ApplicationUser> GetApplicationUser()
        {
            return await _userQueries.FindUserByIdentityNumber(_currentUser.IdentityNumber);
        }
    }
}
