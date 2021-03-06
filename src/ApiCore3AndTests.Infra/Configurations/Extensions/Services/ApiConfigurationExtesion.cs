using ApiCore3AndTests.Infra.Configurations.Extensions.Validations;
using ApiCore3AndTests.Infra.Configurations.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace ApiCore3AndTests.Infra.Configurations.Extensions.Services
{
    public static class ApiConfigurationExtesion
    {
        public static IServiceCollection AddApiConfigurations(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add<CorrelationIdValidateActionFilter>();
            }).AddNewtonsoftJson(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
                    jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    jsonOptions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .AddApplicationPart(typeof(BaseStartup).Assembly)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BaseStartup>())
                .FluentResultToAggregatedError();

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
                options.MimeTypes = new[] {
                    "text/plain",
                    "application/xml",
                    "text/xml",
                    "application/json",
                    "text/json",
                };
            });

            return services;
        }

        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(options => { options.ReportApiVersions = true; });
            return services;
        }
    }
}