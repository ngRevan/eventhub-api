using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace EventHub.Web.Api.Configurations
{
    public static class MvcExtensions
    {
        public static IServiceCollection AddAppMvc(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.CacheProfiles.Add("Never", new CacheProfile() { Location = ResponseCacheLocation.None, NoStore = true });
            });

            services.AddRazorPages();

            return services;
        }
    }
}
