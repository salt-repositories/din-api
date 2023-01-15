using System;
using Din.Application.WebAPI.ConfigurationProviders;
using Din.Application.WebAPI.Content;
using Din.Application.WebAPI.Controller.Middleware;
using Din.Application.WebAPI.Injection.DotNet;
using Din.Application.WebAPI.Injection.SimpleInjector;
using Din.Domain.BackgroundProcessing;
using Din.Domain.BackgroundProcessing.BackgroundTasks.Concrete;
using Din.Domain.Extensions;
using Din.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Din.Application.WebAPI
{
    public class Startup
    {
        private readonly IHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly Container _container;

        public Startup(IHostEnvironment environment)
        {
            _environment = environment;
            
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            builder.AddVaultSecrets(environment);
            builder.AddEnvironmentVariables();

            _configuration = builder.Build();
            _container = new Container();
        }

        // This method gets called first by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("Default", builder =>
            {
                builder
                    .WithOrigins(_configuration.GetSection("CorsOriginDomains").Get<string[]>())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));
            services.RegisterMvcComponents();
            services.RegisterAuthentication(_configuration);
            services.RegisterSwagger();
            services.AddHttpClient();
            services.AddSignalR();
            services.RegisterSignalRCorePipeline(_container);

            IntegrateSimpleInjector(services, _environment);
        }

        // This method gets called second by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseSimpleInjector(_container);
            _container.Verify();

            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                _container.GetService<DinContext>()?.Database.Migrate();
            }

            app = env.IsDevelopment()
                ? app.UseDeveloperExceptionPage()
                : app.UseHsts();

            app.UseCors("Default");
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DinApi V1"); });

            app.UseMiddleware<ExceptionMiddleware>(_container);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ContentHub>("/hubs/content");
            });
        }

        private void IntegrateSimpleInjector(IServiceCollection services, IHostEnvironment env)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            _container.Options.EnableAutoVerification = false;

            services.AddSimpleInjector(_container, options =>
            {
                options
                    .AddAspNetCore()
                    .AddControllerActivation();

                options.AddHostedService<TimedHostedService<ArchiveAuthorizationCodes>>();
                options.AddHostedService<TimedHostedService<UpdateMovieDatabase>>();
                options.AddHostedService<TimedHostedService<UpdateTvShowDatabase>>();
                // options.AddHostedService<TimedHostedService<UpdateContentPlexUrl>>();
                options.AddHostedService<TimedHostedService<UpdateContentPosterUrl>>();
                options.AddHostedService<BackgroundContentQueueProcessor>();
            });
            
            var assemblies = AppDomain.CurrentDomain.GetApplicationAssemblies();

            _container.RegisterMediatr(assemblies);
            _container.RegisterDbContext(_configuration, env);
            _container.RegisterContexts();
            _container.RegisterRepositories();
            _container.RegisterConfigurations(_configuration);
            _container.RegisterClients();
            _container.RegisterStores();
            _container.RegisterHelpers();
            _container.RegisterBackgroundTasks();
            _container.RegisterHubTasks();
        }
    }
}