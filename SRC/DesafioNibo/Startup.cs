using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Desafio.Database;
using Desafio.CrossCutting.Extensions;
using AutoMapperConfiguration = AutoMapper.Configuration;
using AutoMapper;
using BlazorStrap;
using BlazorPrettyCode;
using BlazorStyled;

namespace Desafio
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private static string Environment;
        public Startup(IConfiguration configuration)
        {
            Environment = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            this.Configuration = configuration;
        }

        public Startup(IWebHostEnvironment env)
        {
            Environment = env.EnvironmentName;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureAutoMapper()
        {
            var expression = new AutoMapperConfiguration.MapperConfigurationExpression();
            expression.ConfigureMapping();
            //var config = new MapperConfiguration(expression);
            AutoMapper.Mapper.Initialize(expression);
            //config.AssertConfigurationIsValid();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var isDevelopment = Environment == "Development" ? true : false;
            ConfigureAutoMapper();
            services.ConfigureServices(Configuration);
            services.AddBlazorPrettyCode(defaults =>
            {
                defaults.DefaultTheme = "SolarizedDark";
                defaults.ShowLineNumbers = true;
                defaults.IsDevelopmentMode = true;
            });
            services.AddBootstrapCss();
            services.AddBlazorStyled(isDevelopment);
            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
