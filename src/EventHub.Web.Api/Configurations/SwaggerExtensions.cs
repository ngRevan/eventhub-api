using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventHub.Web.Api.Configurations
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddAppSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "EventHub API", Version = "v1" });
            });

            return services;
        }

        public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "EventHub API V1");
            });

            return app;
        }
    }
}
