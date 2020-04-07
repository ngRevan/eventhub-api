using AutoMapper;
using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.Service.Commands.Handlers.Events;
using EventHub.Service.Commands.Validation.Events;
using EventHub.Service.Queries.Handlers.Events;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace EventHub.Web.Api.Configurations
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAppDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddMediatR(typeof(CreateEventCommandHandler), typeof(EventListQueryHandler));

            services.AddAutoMapper(c => c.ConstructServicesUsing(t => services.BuildServiceProvider().GetService(t)),
                typeof(Service.Commands.Configurations.MappingProfiles.Events.EventMappingProfile),
                typeof(Service.Queries.Configurations.MappingProfiles.Events.EventMappingProfile));

            services.AddFluentValidation(new List<Assembly> { typeof(CreateEventCommandValidator).Assembly });

            return services;
        }
    }
}
