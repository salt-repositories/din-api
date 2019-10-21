using System;
using Din.Application.WebAPI.ConfigurtionProviders;
using Din.Application.WebAPI.Injection.DotNet;
using Din.Application.WebAPI.Injection.SimpleInjector;
using Din.Application.WebAPI.Middleware;
using Din.Application.WebAPI.Movies;
using Din.Domain.BackgroundTasks.Concrete;
using Din.Domain.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Din.Application.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly Container _container = new Container();

        public Startup(IHostingEnvironment environment)
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
            IntegrateSimpleInjector(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app = env.IsDevelopment()
                ? app.UseDeveloperExceptionPage()
                : app.UseHsts();

            InitializeContainer(app, env);
            
            app.UseCors("Default");
            
            InitializeHubs(app);

            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DinApi V1"); });
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

        private void InitializeContainer(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSimpleInjector(_container, options =>
            {
                options.UseMiddleware<ExceptionMiddleware>(app);
                options.UseLogging();
            });

            var assemblies = AppDomain.CurrentDomain.GetApplicationAssemblies();
            
            _container.RegisterMediatr(assemblies);
            _container.RegisterDbContext(Configuration, env);
            _container.RegisterContexts();
            _container.RegisterRepositories();
            _container.RegisterAutoMapper();
            _container.RegisterConfigurations(Configuration);
            _container.RegisterClients();
            _container.RegisterStores();
            _container.RegisterManagers();
            _container.RegisterBackgroundTasks(assemblies);
            _container.RegisterHubTasks();
            
            _container.Verify();
        }

        private void InitializeHubs(IApplicationBuilder app)
        {
            app.UseSignalR(routes => { routes.MapHub<MovieHub>("/hubs/movieHub"); });
        }
    }
}