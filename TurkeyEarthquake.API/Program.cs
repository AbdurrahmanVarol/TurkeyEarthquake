using TurkeyEarthquake.API.Caching.Abstract;
using TurkeyEarthquake.API.Caching.Concrate.Redis;
using TurkeyEarthquake.API.Factories;
using TurkeyEarthquake.API.Services.Abstract;
using TurkeyEarthquake.API.Services.Concrate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEarthquakeService,EarthquakeService>();
builder.Services.AddScoped<ScrapperFactoryBase, ScrapperFactory>();
                                                                   
builder.Services.AddScoped<ICache, RedisCache>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(p=>p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.Run();
