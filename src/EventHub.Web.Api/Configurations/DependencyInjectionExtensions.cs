using AutoMapper;
using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.Service.Queries.Configurations.MappingProfiles.Events;
using EventHub.Service.Queries.Handlers.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventHub.Web.Api.Configurations
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAppDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddMediatR(typeof(EventListQueryHandler));

            services.AddAutoMapper(typeof(EventMappingProfile));

            return services;
        }
    }
}
