﻿using Application;
using Application.Factory;
using Application.IFactory;
using Application.IServices;
using Application.Services;
using AutoMapper;
using DataAccess;
using Dominio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ApiBackend
{
    public class Startup
    {
        private static OpenApiContact contact = new OpenApiContact { Email = "patricio.e.arena@gmail.com", Name = "Patricio Ernesto Antonio Arena" };
        private static OpenApiInfo Info = new OpenApiInfo { Title = "Editor de texto", Version = "v1", Contact = contact };

        private static Context context = Context.POCDbContext; //<= Aca se cambia el contexto de la aplicacion

        private readonly ILogger _Logger;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, ILogger<Startup> logger, IHostEnvironment env)
        {
            Configuration = configuration;
            _Logger = logger;

            configuration["Context"] = Enum.GetName(typeof(Context), context); 

            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddConfiguration(configuration)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IServiceEscritosTexto, ServiceEscritosTexto>();
            services.AddScoped<IAbstractContextFactory, ConcreteContextFactory>();
            services.AddScoped<IAbstractServiceFactory, ConcreteServiceFactory>();

#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            var serviceProvider = services.BuildServiceProvider();
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            var logger = serviceProvider.GetService<ILogger<Startup>>();
            services.AddSingleton(typeof(ILogger), logger);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMvc().AddNewtonsoftJson(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            MapperConfiguration mc = new MapperConfiguration(e =>
            {
               e.AddProfile(new AutoMapperProfileConfiguration());
            });

            IMapper mapper = mc.CreateMapper();

            services.AddSingleton(mapper);
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Info.Version, Info);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });

            //services.Configure<IISOptions>(options =>
            //{
            //    options.AutomaticAuthentication = true;
            //});
            //services.AddAuthentication(IISDefaults.AuthenticationScheme);

            _Logger.LogInformation("Added services in Startup");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
#if DEBUG
            app.UseDeveloperExceptionPage();
            _Logger.LogInformation("In Development environment");
#endif

            app.UseSwagger();

            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{Info.Version}/swagger.json", $"{Info.Title} {Info.Version} - {env.EnvironmentName}");
                c.RoutePrefix = string.Empty;
            });


            app.UseRouting();
            app.UseCors("CorsPolicy");

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
