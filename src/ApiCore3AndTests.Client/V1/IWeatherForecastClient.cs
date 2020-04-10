using ApiCore3AndTests.Domain.Shared;
using Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiCore3AndTests.Client.V1
{
    public interface IWeatherForecastClient
    {
        Task<Result> CreateWeatherForecast(WeatherForecastIncludeCommand weatherForecastCommand, string correlationId, CancellationToken cancellationToken = default);

        Task<Result<IEnumerable<WeatherForecastIncludeCommand>>> GetAllWeatherForecast(string correlationId, CancellationToken cancellationToken = default);
    }
}