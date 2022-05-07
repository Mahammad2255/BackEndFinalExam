﻿using BackEndFinalExam.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFinalExam.DAL
{
    public class BackEndExamDbContext: DbContext
    {
        public BackEndExamDbContext(DbContextOptions<BackEndExamDbContext> options): base(options) { }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders{ get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors{ get; set; }
        public DbSet<ProductImage> ProductImages{ get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Review> Reviews{ get; set; }
        public DbSet<Tag> Tags{ get; set; }
        public DbSet<ProductTag> ProductTags{ get; set; }

        public DbSet<ProductSizeColor> productSizeColors{ get; set; }







    }
}
