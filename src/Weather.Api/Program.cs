using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Weather.Client;
using Weather.Data;
using Weather.Data.Repositories;
using Weather.Data.Repositories.Interfaces;
using Weather.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddSecretsConfiguration(builder.Configuration.GetValue<string>("Secrets:WeatherDB"));

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddMvcCore().AddApiExplorer();

builder.Services.AddTransient<ICsvReader, CsvReader>();
builder.Services.AddTransient<IDatabaseService, DatabaseService>();
builder.Services.AddTransient<IWeatherClient, EnvironmentCanadaClient>();
builder.Services.AddTransient<IStationRepository, StationRepository>();

builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather API", Version = "v1" }));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
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


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();
