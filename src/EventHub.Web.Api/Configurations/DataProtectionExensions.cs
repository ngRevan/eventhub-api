using EventHub.Infrastructure.Configurations;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System.Security.Cryptography.X509Certificates;

namespace EventHub.Web.Api.Configurations
{
    public static class DataProtectionExensions
    {
        public static IServiceCollection AddAppDataProtection(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
        {
            if (!environment.IsDevelopment())
            {
                var dataProtectionSection = new DataProtectionSection();
                configuration.Bind(DataProtectionSection.SectionName, dataProtectionSection);
                var certificate = new X509Certificate2(dataProtectionSection.Certificate.FilePath, dataProtectionSection.Certificate.Password);

                var connectionMultiplexer = services.BuildServiceProvider().GetService<IConnectionMultiplexer>();
                services.AddDataProtection()
                    .PersistKeysToStackExchangeRedis(connectionMultiplexer)
                    .ProtectKeysWithCertificate(certificate);
            }

            return services;
        }
    }
}
