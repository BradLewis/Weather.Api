
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Weather.Client;
using Weather.Data;
using Weather.Data.Repositories;
using Weather.Data.Repositories.Interfaces;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpClient();
        services.AddMvcCore().AddApiExplorer();

        services.AddTransient<ICsvReader, CsvReader>();
        services.AddSingleton<IDatabaseService, DatabaseService>();
        services.AddTransient<IWeatherClient, EnvironmentCanadaClient>();
        services.AddTransient<IStationRepository, StationRepository>();

        services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather API", Version = "v1" }));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseHsts();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseReDoc(c => c.SpecUrl("/swagger/v1/swagger.json"));


        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}