using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.DataAccess.EntityFramework.Models;
using EventHub.Infrastructure.Configurations;
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
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    var googleAuth = configuration.GetSection(GoogleAuthSection.SectionName).Get<GoogleAuthSection>();
                    options.ClientId = googleAuth.ClientId;
                    options.ClientSecret = googleAuth.ClientSecret;
                })
                .AddIdentityServerJwt();

            services.AddAuthorization();

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
