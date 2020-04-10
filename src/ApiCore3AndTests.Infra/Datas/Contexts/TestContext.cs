using ApiCore3AndTests.Domain.Entities;
using ApiCore3AndTests.Infra.Datas.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ApiCore3AndTests.Infra.Datas.Contexts
{
    public class TestContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public TestContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WeatherForecastConfiguration());
        }
    }
}