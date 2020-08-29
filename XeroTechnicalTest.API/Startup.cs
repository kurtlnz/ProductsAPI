using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XeroTechnicalTest.Domain.Services;

namespace XeroTechnicalTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            // var assemblies = new List<Assembly> { Assembly.Load("XeroTechnicalTest.API") };
            // services.AddAutoMapper(_ => _.CreateMissingTypeMaps = true, assemblies);

            services.AddScoped<IProductService, ProductService>();
            
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "XeroTechnicalTest.API";
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}