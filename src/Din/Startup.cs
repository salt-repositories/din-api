using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Din.Config;
using Din.Data;
using Din.Mapping;
using Din.Middleware;
using Din.Service.BackgroundServices.Concrete;
using Din.Service.Clients.Concrete;
using Din.Service.Clients.Interfaces;
using Din.Service.Config.Concrete;
using Din.Service.Config.Interfaces;
using Din.Service.Generators.Concrete;
using Din.Service.Generators.Interfaces;
using Din.Service.Services.Concrete;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Din
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


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
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            services.AddDbContext<DinContext>(options =>
                options.UseMySql(Configuration["Database:ConString"], b => { b.MigrationsAssembly("Din");})
            );

            //API Versioning
            services.AddApiVersioning(o => o.ApiVersionReader = new UrlSegmentApiVersionReader());

            //Inject Configurations
            services.AddSingleton<ITokenConfig>(new TokenConfig(Configuration["Jwt:Issuer"],
                Configuration["Jwt:Key"]));
            services.AddSingleton<IGiphyClientConfig>(new GiphyClientConfig(Configuration["GiphyClient:Url"],
                Configuration["GiphyClient:Key"]));
            services.AddSingleton<IIpStackClientConfig>(new IpStackClientConfig(Configuration["IpStackClient:Url"],
                Configuration["IpStackClient:Key"]));
            services.AddSingleton<IMovieClientConfig>(new MovieClientConfig(Configuration["MovieClient:Url"],
                Configuration["MovieClient:Key"], Configuration["MovieClient:SaveLocation"]));
            services.AddSingleton<ITvShowClientConfig>(new TvShowClientConfig(Configuration["TvShowClient:Url"],
                Configuration["TvShowClient:Key"], Configuration["TvShowClient:SaveLocation"]));
            services.AddSingleton<IUnsplashClientConfig>(new UnsplashClientConfig(Configuration["UnsplashClient:Url"],
                Configuration["UnsplashClient:Key"]));
            services.AddSingleton<ITMDBClientConfig>(new TMDBClientConfig(Configuration["TMDBClient:Key"]));

            //Inject Services
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<ITvShowService, TvShowService>();
            services.AddTransient<IAccountService, AccountService>();

            //Injecting Clients
            services.AddHttpClient<IGiphyClient, GiphyClient>();
            services.AddHttpClient<IIpStackClient, IpStackClient>();
            services.AddHttpClient<IMovieClient, MovieClient>();
            services.AddHttpClient<ITvShowClient, TvShowClient>();
            services.AddHttpClient<IUnsplashClient, UnsplashClient>();

            //Inject Generators
            services.AddSingleton<IMediaGenerator, MediaGenerator>();

            //Background Services
            services.AddSingleton<IHostedService, ContentUpdateService>();

            //Initialize Mapper
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }));
            services.AddSingleton<IMapper>(mapper);


            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "DinApi", Version = "v1"});
                c.OperationFilter<RemoveVersionFromParameter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = "header"
                });
            });
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

            app.UseAuthentication();
            app.UseForwardedHeaders();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DinApi V1"); });
        }
    }
}