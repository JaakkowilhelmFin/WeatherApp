using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp
{
    public static class WeatherService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<(double MinTemp, double MaxTemp, double Rain)> GetWeatherSummary()
        {
            double latitude = 35.6895;  // Default latitude
            double longitude = 139.6917;  // Default longitude

            string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=temperature_2m_max,temperature_2m_min,precipitation_sum&timezone=auto";
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<WeatherResponse>(json);

            return (weatherData.Daily.Temperature2mMin[0], weatherData.Daily.Temperature2mMax[0], weatherData.Daily.PrecipitationSum[0]);
        }
    }
}
