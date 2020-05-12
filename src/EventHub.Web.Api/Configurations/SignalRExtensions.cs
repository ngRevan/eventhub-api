using EventHub.Infrastructure.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventHub.Web.Api.Configurations
{
    public static class SignalRExtensions
    {
        public static IServiceCollection AddAppSignalR(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
        {
            var signalRBuilder = services.AddSignalR()
                .AddMessagePackProtocol();

            if (!environment.IsDevelopment())
            {
                var distributedCacheSection = new DistributedCacheSection();
                configuration.Bind(DistributedCacheSection.SectionName, distributedCacheSection);

                signalRBuilder.AddStackExchangeRedis(distributedCacheSection.Configuration, options =>
                {
                    options.Configuration.ChannelPrefix = distributedCacheSection.InstanceName;
                });
            }

            return services;
        }
    }
}
