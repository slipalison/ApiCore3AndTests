using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ApiCore3AndTests.Infra.Configurations.Swagger
{
    public static class SwaggerMiddlewareExtension
    {
        public static IApplicationBuilder UseVersionedSwagger(this IApplicationBuilder builder, IApiVersionDescriptionProvider versionProvider)
        {
            builder.UseSwagger(o => o.RouteTemplate = "swagger/{documentName}/swagger.json");

            builder.UseSwaggerUI(options =>
            {
                foreach (var description in versionProvider.ApiVersionDescriptions)
                {
                    options.RoutePrefix = "swagger";
                    options.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json", description.GroupName);
                }
            });

            return builder;
        }
    }
}