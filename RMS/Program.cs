using Microsoft.EntityFrameworkCore;
using RMS.AccountRepository;
using RMS.DatabaseContext;
using RMS.StudentCourseRepository;

namespace RMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AccountContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));
            builder.Services.AddTransient<IStudentRepo, StudentRepo>(); //Adding services for student repository
            builder.Services.AddTransient<ICourseRepo, CourseRepo>(); // Adding services for course repository

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
