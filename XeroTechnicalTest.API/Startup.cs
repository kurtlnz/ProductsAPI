using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XeroTechnicalTest.Domain;
using XeroTechnicalTest.Domain.Services;
using XeroTechnicalTest.Persistence.Repositories;

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
            
            services.AddAutoMapper(typeof(Startup));
            
            services.AddDbContext<DataContext>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "XeroTechnicalTest.API";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Kurt Lim",
                        Email = "klim049@gmail.com",
                    };
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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}