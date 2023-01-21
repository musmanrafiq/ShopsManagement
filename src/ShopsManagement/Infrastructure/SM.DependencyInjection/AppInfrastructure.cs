using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using SM.Business.DataServices;
using SM.Business.Interfaces;
using SM.Data;
using SM.Data.Interfaces;

namespace SM.DependencyInjection
{
    public static class AppInfrastructure
    {
        public static void AppDISetup(this IServiceCollection services, IConfiguration configuration)
        {
            // configure entity framework
            services.AddDbContext<StoreManagementDbContext>(
                options => options.
                UseSqlServer(configuration.GetConnectionString("DbConnection")));

            // repositories configuration
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            // all of the custom configurations
            services.AddScoped<IProductService, ProductService>();
        }
    }
}