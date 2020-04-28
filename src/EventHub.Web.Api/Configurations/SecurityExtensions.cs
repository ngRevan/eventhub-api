using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.DataAccess.EntityFramework.Models;
using EventHub.Infrastructure.Authorization;
using EventHub.Infrastructure.Configurations;
using EventHub.Web.Api.Authorization;
using IdentityServer4.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventHub.Web.Api.Configurations
{
    public static class SecurityExtensions
    {
        public static IServiceCollection AddAppSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(c =>
                {
                    c.Clients.Add(new Client
                    {
                        ClientId = "EventHub.Swagger",
                        ClientName = "Swagger UI for EventHub API",
                        AllowedGrantTypes = GrantTypes.Implicit,
                        AllowAccessTokensViaBrowser = true,
                        RedirectUris = { "https://localhost:44300/swagger/oauth2-redirect.html" },
                        AllowedScopes = { "EventHub.Web.ApiAPI" },
                        RequireConsent = false,
                    });
                });

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    var googleAuth = configuration.GetSection(GoogleAuthSection.SectionName).Get<GoogleAuthSection>();
                    options.ClientId = googleAuth.ClientId;
                    options.ClientSecret = googleAuth.ClientSecret;
                })
                .AddIdentityServerJwt();

            services.Configure<JwtBearerOptions>(
                IdentityServerJwtConstants.IdentityServerJwtBearerScheme,
                options =>
                {
                    var onMessageReceived = options.Events.OnMessageReceived;

                    options.Events.OnMessageReceived = async context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/chat"))
                        {
                            context.Token = accessToken;
                        }

                        await onMessageReceived(context);
                    };
                });

            services.AddAuthorization(options =>
            {
                AuthorizationPolicies.AddPolicies(options);
            });

            return services;
        }

        public static IApplicationBuilder UseAppSecurity(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            return app;
        }
    }
}
