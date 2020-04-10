using ApiCore3AndTests.Domain.Interfaces.Repositories;
using ApiCore3AndTests.Infra.Datas.Contexts;
using ApiCore3AndTests.Infra.Datas.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCore3AndTests.Infra.Configurations.Extensions.Services
{
    public static class DatabaseConfigurationExtesion
    {
        public static IServiceCollection ConfigurationDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration["environment"] != "Test")
                services.AddEntityFrameworkSqlServer()
                        .AddDbContextPool<TestContext>(options => options.UseSqlServer(BaseStartup.DatabaseConectionName));

            return services
                .AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
        }
    }
}