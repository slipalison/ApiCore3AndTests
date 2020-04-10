using ApiCore3AndTests.Infra.Configurations.Extensions.Application;
using ApiCore3AndTests.Infra.Configurations.Extensions.Services;
using ApiCore3AndTests.Infra.Configurations.Middlewares;
using ApiCore3AndTests.Infra.Configurations.Swagger;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApiCore3AndTests.Infra.Configurations
{
    public abstract class BaseStartup
    {
        protected BaseStartup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public static string DatabaseConectionName => "TestBase";

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<ILogger>(provider => provider.GetService<ILogger<StartupBase>>())
                .ConfigurationDatabase(Configuration)
                .ConfigurationBusinessService(Configuration)
                .AddApiConfigurations()
                .AddVersioning()
                .AddAutoMapper(typeof(StartupBase).Assembly)
                .AddSwaggerDocumentation()
                .HealthChecksConfiguration(Configuration)
                .GlobalizationConfiguration()
                .AddAuthenticationExtension(Configuration, Environment)
                .AddAuthorizationExtension(Configuration)
                .AddDependencyInjection(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider versionProvider)
        {
            app.DeveloperDependencies(Environment)
                .UseMiddleware<ExceptionMiddleware>()
                .UseRouting()
                .UseResponseCompression()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .HealthCheckConfiguration()
                .UseVersionedSwagger(versionProvider);
        }
    }
}