using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SM.Business.DataServices;
using SM.Business.Interfaces;
using SM.Data;
using SM.Data.Interfaces;
using SM.DependencyInjection.OptionModels;

namespace SM.DependencyInjection
{
    public static class AppInfrastructure
    {
        public static void HandlePendingMigrations(this IServiceProvider services)
        {
            // create services scope object
            using (var scope = services.CreateScope())
            {
                // pull dbcontext from registered services
                var dbContext = scope.ServiceProvider.GetRequiredService<StoreManagementDbContext>();
                // checing if there is any pending migrations in the code
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    // apply the migrations
                    dbContext.Database.Migrate();
                }
            }
        }
        public static void AppDISetup(this IServiceCollection services, IConfiguration configuration)
        {
            // configure entity framework
            services.AddDbContext<StoreManagementDbContext>(
                options => options.
                UseSqlServer(configuration.GetConnectionString("DbConnection")));

            // repositories configuration
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // setting configuration for authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie((cookieOptions) =>
                {
                    cookieOptions.LoginPath = "/Authentication/login";
                    cookieOptions.Cookie = new CookieBuilder
                    { Name = "StoreManagementCookie"};
                });

            // all of the custom configurations
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStoreService, StoreService>();

            // automapper configuration
            services.AddAutoMapper(typeof(BusinessEntityMappings));

            // setting up all the option models
            services.Configure<AccountOption>((option) =>
            {
                // configure admin account for login into the system
                configuration.GetSection("Account").Bind(option);
            });

            // memory cache setup
            services.AddMemoryCache();
        }
    }
}