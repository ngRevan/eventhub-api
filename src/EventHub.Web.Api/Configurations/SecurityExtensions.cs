using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.DataAccess.EntityFramework.Models;
using EventHub.Infrastructure.Configurations;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
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
