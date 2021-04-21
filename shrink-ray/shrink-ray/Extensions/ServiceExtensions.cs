using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.DAL.Interfaces;
using Service;
using Service.Interfaces;
using Repository.DAL;

namespace shrink_ray.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adding CORs to allow cross-origin traffic.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        /// <summary>
        /// Dependency injection for all neccesary activities.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            string mongoConnectionString = Configuration.GetConnectionString("MongoConnectionString");
            services.AddTransient<IRepository>(s => new ShrinkRayUrlRepository(mongoConnectionString, "Url", "ShortUrl"));
            services.AddTransient<IShrinkRayUrlService, ShrinkRayUrlService>();
        }
    }
}