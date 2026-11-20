using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VerticalSliceArchitecture.API.Configuration.Swagger.Filters;

internal class SecurityRequirementFilter(string scheme) : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var metadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;

        if (!metadata.OfType<IAllowAnonymous>().Any())
        {
            var reference = new OpenApiSecuritySchemeReference(scheme, context.Document);
            var requirement = new OpenApiSecurityRequirement();
            requirement.Add(reference, []);

            operation.Security = new List<OpenApiSecurityRequirement>()
            {
                requirement
            };
        }
    }
}