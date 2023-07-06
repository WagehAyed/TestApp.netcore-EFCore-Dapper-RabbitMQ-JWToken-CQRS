using TestApp.Application.Services.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Application.Queries.UserManagement
{
    public class UserQueries : IUserQueries
    {
        private readonly IQueryExecuter _queryExecuter;
        public UserQueries(IQueryExecuter queryExecuter)
        {
              _queryExecuter= queryExecuter;  
        }
        public async Task<ApplicationUser> FindUserByIdentityNumber(string identityNumber)
        {
            return await _queryExecuter.QueryFirstOrDefaultAsync<ApplicationUser>(SQlScripts.FindUserByIdentityNumber, new { identityNumber });
        }
        public async Task<T> FindUserByIdentityNumber<T>(string identityNumber)
        {
            return await _queryExecuter.QueryFirstOrDefaultAsync<T>("SELECT u.* FROM Users u WHERE Status=1 and IdentityNumber = @identityNumber", new { identityNumber });
        }
    }
}
