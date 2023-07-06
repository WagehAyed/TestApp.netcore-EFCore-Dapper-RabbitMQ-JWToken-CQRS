using EFCore.Configrations;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    public class ApplicationDBContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=WAGEHAYED\MS22;Initial catalog=EFCore;User ID=sa;Password=P@ssw0rd;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 3:Domain Model
            //modelBuilder.Entity<AuditEntry>();

            // Fluent Api
            // First Method
            //modelBuilder.Entity<Blog>()
            //    .Property(a => a.Url)
            //    .IsRequired();

            // Second Method
            //new BlogEntityTypeConfigration().Configure(modelBuilder.Entity<Blog>());

            //Third Method
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogEntityTypeConfigration).Assembly);
            //2:Excelud this Model from migration
            //modelBuilder.Ignore<Post>();
            // الجدول هيظل موجود دون التأثير بالتعديلات
            //modelBuilder.Entity<Blog>()
            //    .ToTable("Blogs", a => a.ExcludeFromMigrations());


            //Change Table Name
            //modelBuilder.Entity<Post>()
            //    .ToTable("Posts",schema:"Blogging");

            // add Default Schema for all Tables
            //modelBuilder.HasDefaultSchema("Blogging");

            // not mapped Column to table
            //modelBuilder.Entity<Blog>()
            //    .Ignore(a => a.AddedOn);
            //modelBuilder.Entity<Blog>()
            //    .Property(a => a.Url)
            //    .HasColumnName("BlogURL");


            modelBuilder.Entity<Blog>(blog =>
            {
                blog.Property(b => b.Url).HasColumnType("varchar(200)");
                blog.Property(b => b.Rating).HasColumnType("decimal(5,2)");
            });


            modelBuilder.Entity<Blog>()
                .Ignore("BlogDESC")
                .Ignore("Url")
                .Property(c => c.Url)
                .HasMaxLength(300)
                .HasComment("The Comment URl of Blog");
            //Add PrimaryKey
            modelBuilder.Entity<Book>()
                .HasKey(a => a.BookKey)
                .HasName("PK_BookKey");

            //add Composite Key
            modelBuilder.Entity<Book>()
                .HasKey(a => new { a.Name, a.Author });

            // set Default Value in Column
            modelBuilder.Entity<Blog>()
                .Property(a => a.Rating)
                .HasDefaultValue(2);
            modelBuilder.Entity<Blog>()
                .Property(a => a.CreatedOn)
                .HasDefaultValueSql("getDate()");
            // computed Column
            modelBuilder.Entity<Author>()
                .Property(a => a.DisplayName)
                .HasComputedColumnSql("[LastName]+' ,'+[FirstName]");
            //PrimaryKey Default Value
            modelBuilder.Entity<Category>()
                    .Property(p => p.Id)
                    .ValueGeneratedOnAdd();
        }

        // 2:Domain Model
        public DbSet<Employee> Employees { get; set; }  
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
