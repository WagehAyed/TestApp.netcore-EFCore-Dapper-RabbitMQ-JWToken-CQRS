using Microsoft.EntityFrameworkCore;

namespace EFCore2
{
    public class ApplicationDBContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                               {
            optionsBuilder.UseSqlServer("Data Source=WAGEHAYED\\MS22;Initial catalog=EFCore3;User ID=sa;Password=P@ssw0rd;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasSequence<int>("Seq_OrderNo");
            modelBuilder.Entity<Order>()
                .Property(c => c.OrderNo)
                .HasDefaultValueSql("NEXT VALUE FOR Seq_OrderNo");

            modelBuilder.Entity<Order_test>()
                    .Property(c => c.OrderNo)
                    .HasDefaultValueSql("NEXT VALUE FOR Seq_OrderNo");
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_test> orderTests { get; set; }
        public class Order
        {
            public int Id { get; set; }
            public long OrderNo { get; set; }
            public double Amount { get; set; }
        }
        public class Order_test
        {
            public int Id { get; set; }
            public long OrderNo { get; set; }
            public double Amount { get; set; }
        }
    }
}
