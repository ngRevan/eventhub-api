using EventHub.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventHub.Web.Api.Configurations
{
    public static class OptionsExtensions
    {
        public static IServiceCollection AddAppOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.Configure<GoogleAuthSection>(configuration.GetSection(GoogleAuthSection.SectionName));
            services.Configure<DistributedCacheSection>(configuration.GetSection(DistributedCacheSection.SectionName));
            services.Configure<SwaggerSection>(configuration.GetSection(SwaggerSection.SectionName));

            return services;
        }
    }
}
