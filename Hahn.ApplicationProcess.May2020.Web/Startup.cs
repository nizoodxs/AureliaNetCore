using Hahn.ApplicationProcess.May2020.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Hahn.ApplicationProcess.May2020.Domain.Models;
using Serilog;

namespace Hahn.ApplicationProcess.May2020.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            AppDbConfig dbConfig = new AppDbConfig();
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(dbConfig.DBName));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn API", Version = "v1" });
                c.ExampleFilters();
            });
            services.AddSwaggerExamplesFromAssemblyOf<Applicant>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSerilogRequestLogging();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn API v1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "/api/{action}/{id?}",
                     defaults: new { controller = "Applicant", action = "Index" });
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("API is operational");
                });
            });
        }
    }
}
