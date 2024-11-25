using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WeatherApp
{
    public static class WeatherService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<WeatherResponse> GetWeatherData(double latitude, double longitude)
        {
            string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,wind_speed_10m&hourly=temperature_2m,relative_humidity_2m,wind_speed_10m&timezone=auto";

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API Error: {response.StatusCode}, Content: {errorContent}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<WeatherResponse>(json);
                return weatherData!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve weather data: {ex.Message}");
            }
        }
    }

    // JSON Response Model
    public class WeatherResponse
    {
        public CurrentWeather? Current { get; set; }
        public HourlyWeather? Hourly { get; set; }
    }

    public class CurrentWeather
    {
        public string? Time { get; set; }
        public double Temperature2m { get; set; }
        public double WindSpeed10m { get; set; }
    }

    public class HourlyWeather
    {
        public List<string>? Time { get; set; }
        public List<double>? Temperature2m { get; set; }
        public List<double>? RelativeHumidity2m { get; set; }
        public List<double>? WindSpeed10m { get; set; }
    }

    
}
