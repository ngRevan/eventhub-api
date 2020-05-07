using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace EventHub.Web.Api.Configurations
{
    public static class DataProtectionExensions
    {
        public static IServiceCollection AddAppDataProtection(this IServiceCollection services, IWebHostEnvironment environment)
        {
            if (!environment.IsDevelopment())
            {
                var connectionMultiplexer = services.BuildServiceProvider().GetService<IConnectionMultiplexer>();
                services.AddDataProtection().PersistKeysToStackExchangeRedis(connectionMultiplexer);
            }

            return services;
        }
    }
}
