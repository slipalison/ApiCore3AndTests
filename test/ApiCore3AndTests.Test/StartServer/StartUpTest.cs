using ApiCore3AndTests.Infra.Datas.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ApiCore3AndTests.Test.StartServer
{
    public class StartupTest : Api.Startup
    {
        public StartupTest(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
        {
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider versionProvider)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<TestContext>();
                dbContext.Database.EnsureCreated();
            }
            base.Configure(app, env, versionProvider);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddApplicationPart(Assembly.Load(new AssemblyName("ApiCore3AndTests.Api")));
            services.AddEntityFrameworkSqlite();
            var inMemorySqlite = new SqliteConnection("Data Source=:memory:");
            inMemorySqlite.Open();

            services.AddDbContext<TestContext>(x =>
            {
                x.UseSqlite(inMemorySqlite);
            });
            services.AddDistributedMemoryCache();
            base.ConfigureServices(services);
        }
    }
}
