using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WeatherApp
{
    public static class WeatherService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<(double MinTemp, double MaxTemp, double Rain)> GetWeatherSummary(double latitude, double longitude)
        {
            string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=temperature_2m_max,temperature_2m_min,precipitation_sum&timezone=auto";

            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<WeatherResponse>(json);

            // Ensure the weather data is valid before accessing it
            if (weatherData?.Daily != null)
            {
                return (
                    weatherData.Daily.Temperature2mMin[0],
                    weatherData.Daily.Temperature2mMax[0],
                    weatherData.Daily.PrecipitationSum[0]
                );
            }
            throw new Exception("Invalid weather data received.");
        }
    }

    // JSON Response Model
    public class WeatherResponse
    {
        public Daily? Daily { get; set; }
    }

    public class Daily
    {
        public List<double>? Temperature2mMin { get; set; }
        public List<double>? Temperature2mMax { get; set; }
        public List<double>? PrecipitationSum { get; set; }
    }
}
