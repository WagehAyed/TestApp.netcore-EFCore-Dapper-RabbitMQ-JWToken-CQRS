

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Models
{
    // Domain Model
    public class Blog
    {
        public int Id { get; set; }
        //[Column("BlogURl")]
        //[Required]
        //[Column(TypeName ="varchar('200')")]
        //[Comment("The Url of the blog")]
        public string Url { get; set; }
        //[NotMapped]
        public DateTime AddedOn { get; set; }
        // Navigation Property create  1:Domain Model
        //[NotMapped] //1:Excelud this  Model  from migration
        public List<Post> Posts { get; set; }

        [Column(TypeName ="decimal(5,2)")]
        public decimal Rating { get; set; }
        [MinLength(15),MaxLength(320)]
        public string BlogDESC { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
