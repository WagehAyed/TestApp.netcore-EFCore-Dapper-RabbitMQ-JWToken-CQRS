using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Application.Queries.UserManagement
{
     static class SQlScripts
    {
          internal static string FindUserByIdentityNumber = "select user.* from users as user where user.Status=1 and IdentityNumber=@identityNumber";
 
    }
}
