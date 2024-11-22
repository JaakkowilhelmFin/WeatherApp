using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherApp
{
    public static class WeatherService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<(double MinTemp, double MaxTemp, double Rain)> GetWeatherSummary()
        {
            double latitude = 63.096;
            double longitude = 21.62;

            string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,precipitation,rain,showers&hourly=temperature_2m,wind_speed_1000hPa&daily=weather_code,temperature_2m_max,temperature_2m_min,wind_speed_10m_max&timezone=auto&models=best_match";

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<WeatherResponse>(json);

                double minTemp = weatherData.Daily.Temperature2mMin[0];
                double maxTemp = weatherData.Daily.Temperature2mMax[0];
                double precipitation = weatherData.Current.Precipitation;

                return (minTemp, maxTemp, precipitation);
            }
            catch (HttpRequestException ex)
            {
                
                return (0, 0, 0); 
            }
        }
    }

    public class WeatherResponse
    {
        public CurrentWeather Current { get; set; }
        public DailyWeather Daily { get; set; }
    }

    public class CurrentWeather
    {
        [JsonPropertyName("temperature_2m")]
        public double Temperature2m { get; set; }

        public double Precipitation { get; set; }
    }

    public class DailyWeather
    {
        [JsonPropertyName("temperature_2m_min")]
        public double[] Temperature2mMin { get; set; }

        [JsonPropertyName("temperature_2m_max")]
        public double[] Temperature2mMax { get; set; }
    }
}
