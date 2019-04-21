using Din.Application.WebAPI.Config;
using Din.Application.WebAPI.Injection.DotNet;
using Din.Application.WebAPI.Injection.SimpleInjector;
using Din.Application.WebAPI.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Din.Application.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly Container _container = new Container();

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddEncryptedProvider();
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterDbContext(Configuration);
            services.RegisterMvcComponents();
            services.RegisterAuthentication(Configuration);
            services.RegisterSwagger();
            services.RegisterHttpClients();

            IntegrateSimpleInjector(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            InitializeContainer(app);

            app.UseAuthentication();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DinApi V1"); });
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(_container));

            services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            _container.RegisterMvcControllers(app);

            _container.Register();
            _container.RegisterConfigurations(Configuration);
            _container.RegisterServices();

            _container.AutoCrossWireAspNetComponents(app);
            _container.Verify();
        }
    }
}