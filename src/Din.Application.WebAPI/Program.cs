using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Din.Application.WebAPI
{ 
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSentry(Environment.GetEnvironmentVariable("SENTRY"))
                .UseStartup<Startup>();
    }
}
