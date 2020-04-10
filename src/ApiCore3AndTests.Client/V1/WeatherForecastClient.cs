using ApiCore3AndTests.Domain.Shared;
using Flurl.Http;
using Responses;
using Responses.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiCore3AndTests.Client.V1
{
    public class WeatherForecastClient : AbstractService, IWeatherForecastClient
    {
        public WeatherForecastClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory, nameof(WeatherForecastClient))
        {
        }

        public async Task<Result> CreateWeatherForecast(WeatherForecastIncludeCommand weatherForecastCommand, string correlationId, CancellationToken cancellationToken = default)
        {
            return await FlurlRequest("/api/v1/weatherforecast", correlationId)
                .AllowAnyHttpStatus()
                .PostJsonAsync(weatherForecastCommand, cancellationToken)
                .ReceiveResult();
        }

        public async Task<Result<IEnumerable<WeatherForecastIncludeCommand>>> GetAllWeatherForecast(string correlationId, CancellationToken cancellationToken = default)
        {
            return await FlurlRequest("/api/v1/weatherforecast", correlationId)
                .AllowAnyHttpStatus()
                .GetAsync(cancellationToken)
                .ReceiveResult<IEnumerable<WeatherForecastIncludeCommand>>();
        }
    }
}