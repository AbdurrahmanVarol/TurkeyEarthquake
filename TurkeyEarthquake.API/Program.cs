using System.Reflection;
using TurkeyEarthquake.API.Caching.Abstract;
using TurkeyEarthquake.API.Caching.Concrete.InMemory;
using TurkeyEarthquake.API.Extensions;
using TurkeyEarthquake.API.Factories;
using TurkeyEarthquake.API.Middlewares;
using TurkeyEarthquake.API.Services.Abstract;
using TurkeyEarthquake.API.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddScoped<IEarthquakeService, EarthquakeService>();
builder.Services.AddScoped<ScrapperFactoryBase, ScrapperFactory>();

builder.Services.AddSingleton<ICache, InMemoryCache>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandleMiddleware>();

app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.Run();
