using EventHub.Web.Api.SwaggerGen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace EventHub.Web.Api.Configurations
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddAppSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Name = "oauth2",
                    Type = SecuritySchemeType.OAuth2,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            Scopes = new Dictionary<string, string> { ["EventHub.Web.ApiAPI"] = "EventHub API" },
                            AuthorizationUrl = new Uri("https://localhost:44300/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:44300/connect/token")
                        },
                    }
                });

                options.OperationFilter<OAuth2OperationFilter>();
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
                options.OAuthClientId("EventHub.Swagger");
            });

            return app;
        }
    }
}
