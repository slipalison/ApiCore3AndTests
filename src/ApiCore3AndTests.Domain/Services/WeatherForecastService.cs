using ApiCore3AndTests.Domain.Entities;
using ApiCore3AndTests.Domain.Interfaces.Repositories;
using ApiCore3AndTests.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCore3AndTests.Domain.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;

        public WeatherForecastService(IWeatherForecastRepository weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAll()
        {
            var result = new List<WeatherForecast>();
            await foreach (var dataPoint in _weatherForecastRepository.GetAll())
                result.Add(dataPoint);

            return result;
        }

        public async Task Insert(WeatherForecast weatherForecast)
        {
            await _weatherForecastRepository.Add(weatherForecast);
        }
    }
}