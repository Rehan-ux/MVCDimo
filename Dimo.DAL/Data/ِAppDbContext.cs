using Dimo.DAL.Data.Configurations; 
using Dimo.DAL.Models;
using Dimo.DAL.Models.DepartmentModels;
using Dimo.DAL.Models.EmployeeModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        //هنا مش هعمل   over ride لل   onconfigring  لااني مش هبعت ال connectiom string
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //MultipeActiveResultSets=true لوعايز ارن اكتر من كويري بزود دي 
        //    optionsBuilder.UseSqlServer("Server=.;Database=MVCG01C43;Trusted_Connection=true;");
        //}
        //اي flount APIيرن عندي بعمل ال method 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration<Department>(new DepartmentConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//بستخدم دي علشان لوعندي method كتير
            base.OnModelCreating(modelBuilder); //بعمل call لااني محتاجه flountAPI
           // modelBuilder.Entity<IdentityUser>().ToTable("Users");
           // modelBuilder.Entity<IdentityRole>().ToTable("Roles");  //لوعايزه اغير اسامي table


        }
        public DbSet<Department> Department { get; set; } //table قي الداتا 
       // public object Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<IdentityUser> Users { get; set; }//Dbset for genaric<int>
       // public DbSet<IdentityRole> Roles { get; set; }

    }
}
