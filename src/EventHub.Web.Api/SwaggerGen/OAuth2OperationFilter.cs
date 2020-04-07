using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EventHub.Web.Api.SwaggerGen
{
    public class OAuth2OperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerAttributes = context.MethodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes();
            var actionAttributes = context.MethodInfo.GetCustomAttributes();

            if (controllerAttributes.OfType<AllowAnonymousAttribute>().Any() || actionAttributes.OfType<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            if (!operation.Responses.ContainsKey("401"))
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            }
            if (!operation.Responses.ContainsKey("403"))
            {
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
            }

            operation.Security ??= new List<OpenApiSecurityRequirement>();

            var oAuthRequirements = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new[] { "api" }
                }
            };


            operation.Security.Add(oAuthRequirements);
        }
    }
}
