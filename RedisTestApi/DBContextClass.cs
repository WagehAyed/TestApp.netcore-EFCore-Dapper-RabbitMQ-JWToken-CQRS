using Microsoft.EntityFrameworkCore;

namespace RedisTestApi
{
    public class DBContextClass: DbContext
    {
        public DBContextClass(DbContextOptions<DBContextClass> options):base(options) { 
        }
         public DbSet<Product> Products { get; set; }
    }
}
