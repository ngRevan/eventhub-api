using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EventHub.Web.Api.Configurations
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddMigrosCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("https://localhost:4200", "https://eventhub.ch")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials());
            });

            return services;
        }

        public static IApplicationBuilder UseMigrosCors(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");

            return app;
        }
    }
}
