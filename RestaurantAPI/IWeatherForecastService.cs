namespace RestaurantAPI;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> Get(int quantity, int minTemperature, int maxTemperature);
}