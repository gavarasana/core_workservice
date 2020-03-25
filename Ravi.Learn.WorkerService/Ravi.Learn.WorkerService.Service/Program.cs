using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace Ravi.Learn.WorkerService.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configurationRoot = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .Build();

            Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configurationRoot)
                        .CreateLogger();

            try
            {
                var hostBuilder = CreateHostBuilder(args).Build();
                Log.Information("Host is built.");
                hostBuilder.Run();
            }
            catch (Exception exception)
            {
                 Log.Fatal(exception, "App terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }

           // CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                
                .ConfigureServices((hostContext, services) =>
                {
                    
                    services.AddHostedService<Worker>();
                });
    }
}
