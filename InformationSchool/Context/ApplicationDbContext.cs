using InformationSchool.Model;
using InformationSchool.Model.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            
            builder.Entity<ApplicationUser>().HasIndex(x => x.UserName).IsUnique();
            builder.Entity<Class>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<Course>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<CourseType>().HasIndex(x => x.Name).IsUnique();
            //--------------------------------------add soft deleted global filter 
            builder.Entity<ApplicationUser>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<Class>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<Course>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<CourseType>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<ClassStudent>().HasQueryFilter(s => !s.IsDeleted);


        }

        public DbSet<Class> classes { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<CourseType> courseTypes { get; set; }
        public DbSet<ClassStudent> ClassStudent { get; set; }
    }
}
