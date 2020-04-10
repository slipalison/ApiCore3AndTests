using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace ApiCore3AndTests.Infra.Configurations.Swagger
{
    public static class SwaggerServicesExtension
    {
        /// <summary>
        /// Add swagger documentation service on dependency injection container
        /// </summary>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                // TODO: ApplyIgnoreRelationshipsInNamespace
                // options.SchemaFilter<ApplyIgnoreRelationshipsInNamespace<Ticket>>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = GetApplicationName(),
                        Version = description.ApiVersion.ToString()
                    });
                }

                options.SchemaFilter<CustomSchemaExcludeFilter>();
                options.DocumentFilter<LowerCaseDocumentFilter>();

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "ApiCore3AndTests.Api.xml");
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        private static string GetApplicationName()
            => Assembly.GetExecutingAssembly().GetName().Name.Replace(".", " ");
    }
}