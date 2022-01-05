using System;
using Din.Application.WebAPI.ConfigurationProviders;
using Din.Application.WebAPI.Content;
using Din.Application.WebAPI.Injection.DotNet;
using Din.Application.WebAPI.Injection.SimpleInjector;
using Din.Application.WebAPI.Middleware;
using Din.Domain.BackgroundProcessing;
using Din.Domain.BackgroundProcessing.BackgroundQueues.Concrete;
using Din.Domain.Extensions;
using Din.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
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
        public IConfiguration Configuration { get; }
        private readonly Container _container = new Container();

        public Startup(IHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            builder.AddVaultSecrets(environment);
            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("Default", builder =>
            {
                builder
                    .WithOrigins(Configuration.GetSection("CorsOriginDomains").Get<string[]>())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));
            services.RegisterMvcComponents();
            services.RegisterAuthentication(Configuration);
            services.RegisterSwagger();
            services.AddHttpClient();
            services.AddSignalR();
            services.RegisterSignalRCorePipeline(_container);
            services.AddSingleton<IHostedService>(new BackgroundTaskProcessor(_container));
            services.AddSingleton<IHostedService>(new BackgroundContentQueueProcessor(_container));

            IntegrateSimpleInjector(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            InitializeContainer(app, env);
            
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

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();
            });
        }

        private void InitializeContainer(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseSimpleInjector(_container);

            var assemblies = AppDomain.CurrentDomain.GetApplicationAssemblies();
            
            _container.RegisterMediatr(assemblies);
            _container.RegisterDbContext(Configuration, env);
            _container.RegisterContexts();
            _container.RegisterRepositories();
            _container.RegisterAutoMapper(assemblies);
            _container.RegisterConfigurations(Configuration);
            _container.RegisterClients();
            _container.RegisterStores();
            _container.RegisterHelpers();
            _container.RegisterBackgroundTasks(assemblies);
            _container.RegisterHubTasks();

            
            _container.Verify();
        }
    }
}