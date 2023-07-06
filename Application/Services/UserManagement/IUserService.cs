using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Application.Services.UserManagement
{
    public interface IUserService
    {
        Task<ApplicationUser> GetApplicationUser();
    }
}
