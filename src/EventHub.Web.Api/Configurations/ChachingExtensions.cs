using EventHub.Infrastructure.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace EventHub.Web.Api.Configurations
{
    public static class ChachingExtensions
    {
        public static IServiceCollection AddAppCaching(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
        {

            if (environment.IsDevelopment())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                var distributedCacheSection = new DistributedCacheSection();
                configuration.Bind(DistributedCacheSection.SectionName, distributedCacheSection);

                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = distributedCacheSection.Configuration;
                    options.InstanceName = distributedCacheSection.InstanceName;
                });

                services.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect($"{distributedCacheSection.Configuration},channelPrefix={distributedCacheSection.InstanceName}"));
            }

            return services;
        }
    }
}
