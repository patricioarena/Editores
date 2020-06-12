using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using ApiAngular.Authorization.AuthorizationPolicies;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

namespace ApiAngular
{
    public class Startup
    {
        private readonly ILogger _Logger;
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, ILogger<Startup> logger, IHostEnvironment env)
        {
            Configuration = configuration;
            _Logger = logger;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            var serviceProvider = services.BuildServiceProvider();
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            var logger = serviceProvider.GetService<ILogger<Startup>>();
            services.AddSingleton(typeof(ILogger), logger);

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMvc().AddNewtonsoftJson(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                   builder => builder
                   .AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod());
            });

            OpenApiContact contact = new OpenApiContact { Email = "patricio.e.arena@gmail.com", Name = "Patricio Ernesto Antonio Arena" };
            OpenApiInfo Info = new OpenApiInfo { Title = Configuration.GetSection("SwaggerOptions:Description").Value, Version = Configuration.GetSection("SwaggerOptions:Version").Value, Contact = contact };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Info.Version, Info);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            //services.Configure<IISOptions>(options =>
            //{
            //    options.AutomaticAuthentication = true;
            //});
            //services.AddAuthentication(IISDefaults.AuthenticationScheme);

            //services.AddAuthorizationPolicies(Configuration); //importante!!

            _Logger.LogInformation("Added services in Startup");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
#if DEBUG
            app.UseDeveloperExceptionPage();
            _Logger.LogInformation("In Development environment");
#endif

            app.UseHsts();
            app.UseSwagger();
            app.UseHttpsRedirection();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(Configuration.GetSection("SwaggerOptions:UIEndpoint").Value, $"{ env.EnvironmentName} - {Configuration.GetSection("SwaggerOptions:Version").Value}");
                option.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseCors("AllowAll");

            //app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
