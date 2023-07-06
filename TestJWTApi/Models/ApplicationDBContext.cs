

using Microsoft.EntityFrameworkCore;

namespace TestJWTApi.Models
{
    //public class ApplicationDBContext :IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options) { 
    
    //    }
         
    //}
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {
            
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }

    }
}
 