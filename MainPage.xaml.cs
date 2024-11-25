using Microsoft.Maui.Controls;
using System;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetWeatherData();
        }

        public async void GetWeatherData()
        {
            double latitude = 63.096;  // Example: Vaasa, Finland
            double longitude = 21.6158;

            try
            {
                // Fetch the weather data
                var weatherData = await WeatherService.GetWeatherData(latitude, longitude);

                // Display current weather (if available)
                var current = weatherData.Current;
                if (current != null)
                {
                    minTempLabel.Text = $"Current Temp: {current.Temperature2m}°C";
                    maxTempLabel.Text = $"Wind Speed: {current.WindSpeed10m} m/s";
                }

                // Display hourly weather (first entry as an example)
                var hourly = weatherData.Hourly;
                if (hourly != null && hourly.Temperature2m != null && hourly.Temperature2m.Count > 0)
                {
                    rainLabel.Text = $"First Hour Temp: {hourly.Temperature2m[0]}°C, Humidity: {hourly.RelativeHumidity2m[0]}%";
                }
            }
            catch (Exception ex)
            {
                // Display error message
                await DisplayAlert("Error", $"Unable to retrieve weather data: {ex.Message}", "OK");
            }
        }
    }
}
