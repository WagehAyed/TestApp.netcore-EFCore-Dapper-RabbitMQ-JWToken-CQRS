using Dapper;
using DapperCRUD.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Z.Dapper.Plus;

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


        [HttpGet("Employees")]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            IEnumerable <Employee> employees= null;
            using (SqlConnection connection=new SqlConnection(Connection))
            {
                var sql = "SELECT e.EmployeeID, e.Name as 'employeeName', e.DepartmentID, d.DepartmentID, d.Name  as 'departmentName'" +
            "FROM Employee e INNER JOIN Department d ON e.DepartmentID = d.DepartmentID ";

                  employees =await connection.QueryAsync<Employee, Department, Employee>(
                    sql,
                    (emp, dept) =>
                    {
                        emp.Department = dept;
                        return emp;
                    }, 
                    splitOn: "DepartmentID"
                );
            }
            return employees;
        }

        [HttpPost("BulKMergeCategory")]
        public async Task<bool> BulKMergeCategory()
        {
            using (SqlConnection connection=new SqlConnection(Connection))
            {
                var sqlScript = " INSERT INTO [dbo].[Category] ([CategoryName], [Description]) VALUES " +
                    "(  '', 'Soft drinks, coffees, teas, beers, and ales')," +
                    "( '', 'Sweet and savory sauces, relishes, spreads, and seasonings')," +
                    "( '', '');";
                //"( 'Dairy Products', 'Cheeses'),\"+" +
                //"( 'Grains/Cereals', 'Breads, crackers, pasta, and cereal'), " +
                //"( 'Meat/Poultry', 'Prepared meats')," +
                //"( 'Produce', 'Dried fruit and bean curd')," +
                //"( 'Seafood', 'Seaweed and fish');";
                //DapperPlusManager.Entity<Category>("Category_IgnoreAditProperty")
                //    .IgnoreOnMergeUpdate(x => new { x.CategoryName, x.Description });

                 var categories= new List<Category>
                 {
                     new Category{CategoryId=22,CategoryName="Beverages",Description="Soft drinks, coffees, teas, beers, and ales" },
                     new Category{CategoryId=23,CategoryName="Condiments",Description="Sweet and savory sauces, relishes, spreads, and seasonings" },
                     new Category{CategoryId=24,CategoryName="Confections",Description="Desserts, candies, and sweet breads" },
                     new Category{CategoryId=25,CategoryName="Dairy Products",Description="Cheeses" }

                 };
                var result = connection.BulkMerge("Category", categories);
                return result.CurrentItem.Any();    
            }


                return false;
        }


        // Mappster
        [HttpGet("MappPerson")]
        public async Task<List<PersonDTO>> GetPersonDTO()
        {
            using (SqlConnection connection=new SqlConnection(Connection))
            {
                var sqlScript = "select p.*,a.* " +
                    "from Person as p " +
                    "join address as a on a.Id= p.AddressId";
                var _person = await connection.QueryAsync<Person, Address, Person>(sqlScript, (Person, Address) =>
                {
                    Person.Address = Address;
                    return Person;
                });
                var _personDTO = _person.First().Adapt<PersonDTO>();

                 var _personDTO2 = _person.AsQueryable().ProjectToType<PersonDTO>();

                return _personDTO2.ToList() ;
            }

            return null;
        }

    }
}
