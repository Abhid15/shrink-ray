using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace shrink_ray.Extensions
{
    public static class ConfigureExtensions
    {
        /// <summary>
        /// Global Error handling added to the pipeline.
        /// </summary>
        /// <param name="app"></param>
        public static void AddGlobalExceptionHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;

                        await context.Response.WriteAsync(new ErrorModel()
                        {
                            StatusCode = 500,
                            ErrorMessage = ex.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}