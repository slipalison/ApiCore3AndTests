using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ApiCore3AndTests.Infra.Configurations.Extensions.Services
{
    public static class HealthChecksConfigurationExtension
    {
        public static IServiceCollection HealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlServer(configuration.GetConnectionString(BaseStartup.DatabaseConectionName));

            return services;
        }
    }
}