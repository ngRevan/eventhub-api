using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EventHub.Web.Api.Configurations
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddMigrosCors(this IServiceCollection services)
        {
            services.AddCors();

            return services;
        }

        public static IApplicationBuilder UseMigrosCors(this IApplicationBuilder app)
        {
            app.UseCors(configurePolicy =>
            {
                configurePolicy.AllowAnyOrigin();
                configurePolicy.AllowAnyHeader();
                configurePolicy.AllowAnyMethod();
                configurePolicy.WithExposedHeaders("Content-Disposition", "Content-Length");
            });

            return app;
        }
    }
}
