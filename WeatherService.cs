using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<WeatherData> GetWeatherAsync(double latitude, double longitude)
    {
        string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m&hourly=temperature_2m,wind_speed_1000hPa&timezone=auto&models=best_match";
        var response = await _httpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<WeatherData>(response);
    }
}

public class WeatherData
{
    public CurrentWeather Current { get; set; }
    public HourlyWeather Hourly { get; set; }
}

public class CurrentWeather
{
    public double Temperature2m { get; set; }
}

public class HourlyWeather
{
    public List<string> Time { get; set; } // Add time for hourly data
    public List<double> Temperature2m { get; set; }
    public List<double> WindSpeed1000hPa { get; set; }
}