using ApiCore3AndTests.Domain.Entities;
using ApiCore3AndTests.Domain.Interfaces.Repositories;
using ApiCore3AndTests.Infra.Datas.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCore3AndTests.Infra.Datas.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly TestContext _context;

        public WeatherForecastRepository(TestContext testContext)
        {
            _context = testContext;
        }

        public async Task Add(WeatherForecast weatherForecast)
        {
            await _context.WeatherForecasts.AddAsync(weatherForecast);
            await _context.SaveChangesAsync();
        }

        public async Task<IAsyncEnumerable<WeatherForecast>> GetAll()
        {
            return _context.WeatherForecasts.AsAsyncEnumerable();
        }
    }
}