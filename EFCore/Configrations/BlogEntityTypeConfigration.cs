﻿using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Configrations
{
    public class BlogEntityTypeConfigration:IEntityTypeConfiguration<Blog>
    { 

        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(a => a.Url)
               .IsRequired();
        }
    }
}
