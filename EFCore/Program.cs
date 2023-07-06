



using EFCore.Models;

namespace EFCore
{
    public class Program
    {
        static void Main(string[] args)
        {
             var _context=new ApplicationDBContext();

            var employee=new Employee()
            {
                Name="Wageh Ayed"
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

        }
    }
}