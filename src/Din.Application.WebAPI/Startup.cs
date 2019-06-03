using System;
using Din.Application.WebAPI.ConfigurtionProviders;
using Din.Application.WebAPI.Injection.DotNet;
using Din.Application.WebAPI.Injection.SimpleInjector;
using Din.Application.WebAPI.Middleware;
using Din.Domain.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.RegisterMvcComponents();
            services.RegisterAuthentication(Configuration);
            services.RegisterSwagger();
            services.AddHttpClient();

            IntegrateSimpleInjector(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app = env.IsDevelopment()
                ? app.UseDeveloperExceptionPage()
                : app.UseHsts();

            InitializeContainer(app, env);

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

            _container.RegisterMediatr(AppDomain.CurrentDomain.GetApplicationAssemblies());
            _container.RegisterDbContext(Configuration, env);
            _container.RegisterContexts();
            _container.RegisterRepositories();
            _container.RegisterAutoMapper();
            _container.RegisterConfigurations(Configuration);
            _container.RegisterServices();

            _container.Verify();
        }
    }
}