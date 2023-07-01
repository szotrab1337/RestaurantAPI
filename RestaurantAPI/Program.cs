using RestaurantAPI;
using RestaurantAPI.Entites;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddScoped<RestaurantSeeder>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var restaurantSeeder = scope.ServiceProvider.GetService<RestaurantSeeder>();
restaurantSeeder?.Seed();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
