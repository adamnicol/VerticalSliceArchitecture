using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VerticalSliceArchitecture.API.Filters;

public sealed class FeatureTagOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var namespaceName = context.MethodInfo?.DeclaringType?.Namespace;

        if (!string.IsNullOrWhiteSpace(namespaceName))
        {
            var segments = namespaceName.Split('.');
            if (segments.Length > 1)
            {
                var tag = segments[segments.Length - 2];

                operation.Tags?.Clear();
                operation.Tags?.Add(new OpenApiTagReference(tag));
            }
        }
    }
}
