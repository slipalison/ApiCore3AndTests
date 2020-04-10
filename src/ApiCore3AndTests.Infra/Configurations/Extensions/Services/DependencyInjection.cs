using ApiCore3AndTests.Domain.Interfaces.Services;
using ApiCore3AndTests.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCore3AndTests.Infra.Configurations.Extensions.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddScoped<IWeatherForecastService, WeatherForecastService>();
        }
    }
}