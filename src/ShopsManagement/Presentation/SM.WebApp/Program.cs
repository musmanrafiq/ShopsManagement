using Microsoft.EntityFrameworkCore;
using SM.Business.DataServices;
using SM.Business.Interfaces;
using SM.Data;

namespace SM.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // configure entity framework
            builder.Services.AddDbContext<StoreManagementDbContext>(
                options => options.UseSqlServer("Data Source=localhost; Database=SMSystem;Integrated Security=SSPI;TrustServerCertificate=True;"));

            // all of the custom configurations
            builder.Services.AddScoped<IProductService , ProductService>();

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