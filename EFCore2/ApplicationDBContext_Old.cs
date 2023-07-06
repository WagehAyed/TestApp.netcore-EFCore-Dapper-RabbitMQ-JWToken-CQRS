//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace EFCore2
//{
//    public class ApplicationDBContext_Old : DbContext
//    {
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer("Data Source=WAGEHAYED\\MS22;Initial catalog=EFCore2;User ID=sa;Password=P@ssw0rd;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
//            base.OnConfiguring(optionsBuilder);

//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//            // Relationship one to one
//            modelBuilder.Entity<Blog>()
//                .HasOne(b => b.BlogImages)
//                .WithOne(c => c.Blog)
//                .HasForeignKey<BlogImage>(b => b.BlogForeignKey)
//                .HasConstraintName("Posts_Blog_BlogId");
//            // Relationship Many to one or one to many
//            //modelBuilder.Entity<Blog>()
//            //    .HasMany(b => b.Posts)
//            //    .WithOne(c => c.Blog);

//            modelBuilder.Entity<Post>()
//                .HasOne<Blog>()
//                .WithMany()
//                .HasForeignKey(b => b.BlogId);

//            // Relation one to Many 
//            modelBuilder.Entity<RecordOfSale>()
//                .HasOne(s => s.Car)
//                .WithMany(c => c.SaleHistory)
//                .HasForeignKey(s => new { s.LicensePlate, s.CarState })
//                .HasPrincipalKey(p => new { p.LicensePlate, p.State });


//            //Relation Many to many
//            //modelBuilder.Entity<Post>()
//            //    .HasMany(c => c.Tags)
//            //    .WithMany(c => c.Posts)
//            //    //.UsingEntity(j => j.ToTable("PostsTags"));
//            //    .UsingEntity<PostTag>(
//            //    j => j
//            //    .HasOne(pt => pt.Tag)
//            //    .WithMany(t => t.PostsTags)
//            //    .HasForeignKey(pt => pt.TagId)
//            //    ,
//            //    j => j
//            //      .HasOne(pt => pt.Post)
//            //      .WithMany(t => t.PostsTags)
//            //      .HasForeignKey(pt => pt.PostId),
//            //      j =>
//            //      {
//            //          j.Property(pt => pt.AddOn).HasDefaultValueSql("getDate()");
//            //          j.HasKey(t => new { t.TagId, t.PostId });

//            //      }

//            //    );
//            //indirect many to many relatioinship posts and Tags
//            modelBuilder.Entity<PostTag>()
//                .HasKey(t =>new { t.TagId,t.PostId});
//            modelBuilder.Entity<PostTag>()
//                .HasOne(pt => pt.Post)
//                .WithMany(p => p.PostsTags)
//                .HasForeignKey(pt=>pt.PostId);
//            modelBuilder.Entity<PostTag>()
//             .HasOne(pt => pt.Tag)
//             .WithMany(p => p.PostsTags)
//             .HasForeignKey(pt=>pt.TagId);

//            //create index
//            modelBuilder.Entity<Person>()
//                .HasIndex(c => new { c.FirstName, c.LastName })
//                .IsUnique()
//                .HasFilter("FirstName_LastName is composite key") 
//                .HasName("Indx_Person_FirstName_LastName");
//        }
//        public DbSet<Blog> Blogs { get; set; }
//        public DbSet<BlogImage> BlogImages { get; set; }
//        public DbSet<Post> Posts { get; set; }
//        public DbSet<Person> People { get; set; }
//    }




//    public class Blog
//    {
//        public int Id { get; set; } //Priniple key
//        [Required, MaxLength(150)]
//        public string Url { get; set; }
//        public BlogImage BlogImages { get; set; }
//        //public List<Post> Posts { get; set; } //collection Navigation Property
//    }
//    public class BlogImage
//    {
//        public int Id { get; set; }
//        public string Image { get; set; }
//        [Required, MaxLength(250)]
//        public string Caption { get; set; }
//        public int BlogForeignKey { get; set; }
//        //[ForeignKey("BlogForeignKey")]
//        public Blog Blog { get; set; }

//    }

//    public class Post
//    {
//        public int Id { get; set; }
//        public string Title { get; set; }
//        public string Content { get; set; }
//        public int BlogId { get; set; } // Foriegn key
//        //public Blog Blog { get; set; }  // reference Navigation Property
//        //public ICollection<Tag> Tags { get; set; }
//        public List<PostTag> PostsTags { get; set; }
//    }



//    #region example relation one to many

//    public class car
//    {
//        public int CarId { get; set; }
//        public string LicensePlate { get; set; }
//        public string State { get; set; }
//        public string Make { get; set; }
//        public string Model { get; set; }
//        public List<RecordOfSale> SaleHistory { get; set; }
//    }

//    public class RecordOfSale
//    {
//        public int RecordOfSaleId { get; set; }
//        public DateTime DateSold { get; set; }
//        public decimal Price { get; set; }
//        public string LicensePlate { get; set; }
//        public string CarState { get; set; }
//        public car Car { get; set; }
//    }
//    #endregion
//    #region example relation many to many
//    public class Tag
//    {
//        public string TagId { get; set; }
//        //public ICollection<Post> Posts { get; set; }
//        public List<PostTag> PostsTags { get; set; }
//    }
//    public class PostTag
//    {
//        public int PostId { get; set; }
//        public Post Post { get; set; }
//        public string TagId { get; set; }
//        public Tag Tag { get; set; }
//        public DateTime AddOn { get; set; }
//    }
//    #endregion

//    #region Create Index

//    //[Index(nameof(FirstName),nameof(LastName),IsUnique =true,Name = "Indx_Person_FirstName_LastName")]
//    public class Person
//    {
//        public int Id { get; set; }
//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//    }
//    #endregion


//}
