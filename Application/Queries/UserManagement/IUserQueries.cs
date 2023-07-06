using TestApp.Application.Services.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Application.Queries.UserManagement
{
    public interface IUserQueries
    {
        Task<ApplicationUser> FindUserByIdentityNumber(string identityNumber);
        Task<T> FindUserByIdentityNumber<T>(string identityNumber); 
    }
}
