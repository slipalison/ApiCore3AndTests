using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;

namespace ApiCore3AndTests.Api
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, configuration) =>
                    {
                        configuration.AddEnvironmentVariables();
                        if (!context.HostingEnvironment.IsDevelopment() && context.HostingEnvironment.EnvironmentName != "Test")
                        {
                        }
                    })
                    .UseSerilog((hostingContext, cfg) =>
                    {
                        cfg.Enrich.FromLogContext();
                        cfg.Enrich.WithExceptionDetails();
                        cfg.Enrich.WithMachineName();
                        cfg.WriteTo.Console();
                        cfg.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Verbose);

                        cfg.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(hostingContext.Configuration.GetConnectionString("Elastic")))
                        {
                            AutoRegisterTemplate = true,
                            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                            CustomFormatter = new ElasticsearchJsonFormatter(),
                            BufferBaseFilename = "./logs/log",
                            IndexFormat = hostingContext.Configuration["elastic:index"].ToString()
                        });
                    })
                    .UseStartup<Startup>();
                });
    }
}