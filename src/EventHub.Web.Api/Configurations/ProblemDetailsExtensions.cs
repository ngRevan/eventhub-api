using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventHub.Web.Api.Configurations
{
    public static class ProblemDetailsExtensions
    {
        public static IServiceCollection AddAppProblemDetails(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (context, ex) => environment.IsDevelopment();

                options.MapToStatusCode<KeyNotFoundException>(StatusCodes.Status404NotFound);
                options.Map<ValidationException>((context, ex) => CreateValidationProblemDetails(ex));
                options.MapToStatusCode<InvalidOperationException>(StatusCodes.Status400BadRequest);
                options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
                options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            });

            return services;
        }

        public static IApplicationBuilder UseAppProblemDetails(this IApplicationBuilder app)
        {
            app.UseProblemDetails();

            return app;
        }

        private static ValidationProblemDetails CreateValidationProblemDetails(ValidationException validationException)
        {
            var errors = validationException.Errors.GroupBy(f => f.PropertyName, f => f.ErrorMessage).ToDictionary(g => g.Key, g => g.ToArray()) ?? new Dictionary<string, string[]>();
            var problemDetails = new ValidationProblemDetails(errors)
            {
                Type = $"https://httpstatuses.com/{StatusCodes.Status400BadRequest}",
                Title = "One or more validation errors occured",
                Status = StatusCodes.Status400BadRequest,
            };

            return problemDetails;
        }
    }
}
