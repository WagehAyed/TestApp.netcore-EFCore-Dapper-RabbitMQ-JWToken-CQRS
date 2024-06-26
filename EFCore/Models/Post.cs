﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Models
{
    //[Table("Posts",Schema ="Bloging")]  // Change Table Name
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        //Navigation Property create  1:Domain Model
        public Blog Blog { get; set; }
    }
}
