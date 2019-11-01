using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Weather.Client;
using Weather.Data;
using Weather.Data.Repositories;
using Weather.Data.Repositories.Interfaces;
using Microsoft.OpenApi.Models;
using System.Net.Http;

namespace Weather.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpClient();

            services.AddMvcCore().AddApiExplorer();

            services.AddTransient<IDatabaseService>(s =>
                new DatabaseService(Configuration.GetConnectionString("WeatherDB"))
            );
            services.AddTransient<IWeatherClient>(s =>
                new EnvironmentCanadaClient(
                    s.GetRequiredService<IHttpClientFactory>(),
                    Configuration.GetValue<string>("Endpoints:EnvironmentCanada")
                )
            );
            services.AddTransient<IStationRepository, StationRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseReDoc(c =>
            {
                c.SpecUrl("/swagger/v1/swagger.json");
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
