using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Weather.Client;
using Weather.Data;
using Weather.Data.Repositories;
using Weather.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.AddMvcCore().AddApiExplorer();

builder.Services.AddTransient<ICsvReader, CsvReader>();
builder.Services.AddTransient<IDatabaseService, DatabaseService>();
builder.Services.AddTransient<IWeatherClient, EnvironmentCanadaClient>();
builder.Services.AddTransient<IStationRepository, StationRepository>();

builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather API", Version = "v1" }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
