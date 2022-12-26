using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Din.Application.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Sentry(options =>
                {
                    options.Dsn = "https://196e434f7ff1499eba5a3a7b6ebdea71@o237728.ingest.sentry.io/1406081";
                    options.AttachStacktrace = true;
                    options.Debug = true;
                    options.MinimumBreadcrumbLevel = LogEventLevel.Debug;
                    options.MinimumEventLevel = LogEventLevel.Warning;
                })
                .CreateLogger();

            try
            {
                Log.Information("Starting host");
                BuildHost(args).Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHost BuildHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHost =>
                {
                    webHost.UseStartup<Startup>();
                })
                .UseSerilog()
                .Build();
    }
}