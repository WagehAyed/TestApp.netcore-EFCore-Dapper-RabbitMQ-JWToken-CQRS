
using static EFCore2.ApplicationDBContext;

namespace EFCore2
{
    public class Program
    {
        static void Main(string[] args)
        {
            var _context = new ApplicationDBContext();
            var order = new Order
            {
                Amount=100
            };
            _context.Orders.Add(order);

            var orderTest = new Order_test()
            {
                Amount = 250
            };
            _context.orderTests.Add(orderTest);
            _context.SaveChanges();


        } 

         
    }
}