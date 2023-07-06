using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperController : ControllerBase
    {
        string Connection = "Data Source=WAGEHAYED\\MS22;Initial Catalog=DapperDB;User Id=sa;Password=P@ssw0rd;";
        [HttpGet("Logins")]
        public async Task<IEnumerable<Login>> Get()
        {
            IEnumerable<Login> temp;
            using (SqlConnection connection=new SqlConnection(Connection))
            {
                 var Params= new DynamicParameters();
                Params.Add("@flag", "read");
                temp=await connection.QueryAsync<Login>("LoginProcedure", Params,commandType: CommandType.StoredProcedure);
            }
                return temp;
        }
        [HttpDelete("Delete")]
        public async Task<bool> Delete(int id)
        {
            int result = 0;
            using (SqlConnection connection=new SqlConnection(Connection))
            {
                var Params= new DynamicParameters();    
                Params.Add("@id", id);
                Params.Add("@flag", "delete");
                result =await connection.ExecuteAsync("LoginProcedure", param: Params, commandType: CommandType.StoredProcedure);
            }
            return (result==1?true:false);
        }
        [HttpPost("CreateLogin")]
        public async Task<int> Post([FromBody] Login viewModel)
        {
            var result=0;
            using (var connection =new SqlConnection(Connection))
            {
                string command=" Insert into Login (Email,Password) values('"+viewModel.Email+ "','" + viewModel.Password+"');";
                result=await connection.ExecuteAsync(command); 
            }
            return result;
        }

        [HttpPut("UpdateLogin")]
        public async Task<int> Put([FromBody] Login viewModel)
        {
            var result=0;
            using (var connection=new SqlConnection(Connection))
            {
                var command = "Update login set Email='" + viewModel.Email + "',Password='" + viewModel.Password + "' where id="+viewModel.Id;
                result =await  connection.ExecuteAsync(command);
            }
            return result;
        }

    }
}
