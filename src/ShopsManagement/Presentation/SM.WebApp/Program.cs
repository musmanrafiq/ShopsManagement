using SM.Data;
using SM.DependencyInjection;
using SM.WebApp.Middlewares;
using System;

namespace SM.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // let the razor views complied on save
            builder.Services.AddMvc().AddRazorRuntimeCompilation();
            // All application DI configurations 
            builder.Services.AppDISetup(builder.Configuration);
            // register custom exception middleware here
            builder.Services.AddTransient<ExceptionMiddleware>();
            var app = builder.Build();

            // check if there is any pending migration then apply on app start
            app.Services.HandlePendingMigrations();

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

            app.UseAuthentication();
            app.UseAuthorization();

            // adding middleware for exception
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}