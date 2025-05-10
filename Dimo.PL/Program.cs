using Dimo.BLL.Profiles;
using Dimo.BLL.Services.AttatchMentServices;
using Dimo.BLL.Services.Clases;
using Dimo.BLL.Services.Interfaces;
using Dimo.DAL.Data;
using Dimo.DAL.Data.Repositries.Classes;
using Dimo.DAL.Data.Repositries.Interfacies;
using Dimo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;


namespace Dimo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
           
           // builder.Services.AddScoped<AppDbContext>();//alow Dependanceingction for AppDbcontext

            //??????? ?? ????? ???? ???? ????? ?? connectionstring 
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                //??? ????? ?????? ???? ?? connetionstring 
                //????? regester ?? appdbcontext / dbcontectoption
                options.UseLazyLoadingProxies();
            });
            //???? regester ?   dependancyEngiction
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepositry>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            // builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly); ?? ????? ??????? ?? ?????
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
   //         builder.Services.AddScoped<UserManager<ApplicationUser>>();
			//builder.Services.AddScoped<SignInManager<ApplicationUser>>();
   //         builder.Services.AddScoped<RoleManager<IdentityRole>>();
   //بدل التلاته هستخدم method تعملهم كلك regester

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Option=>
            {
                //Option.User.RequireUniqueEmail=true;
                //Option.Password.RequireUppercase = true;
                //Option.Password.RequireLowercase = true;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();




			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            
            app.UseAuthentication(); //Omar
            app.UseAuthorization();//admin

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            app.Run();
            //using DbContext dbContext = new DbContext();
            //dbContext.departments.add();
        }
    }
}
