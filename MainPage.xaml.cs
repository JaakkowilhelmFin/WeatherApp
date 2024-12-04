using Microsoft.Maui.Controls;
using System;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        private readonly WeatherService _weatherService;

        public MainPage()
        {
            InitializeComponent();
            _weatherService = new WeatherService();
            GetWeatherData();
        }

        public async void GetWeatherData()
        {
            double latitude = 63.63;  // Update these values as needed
            double longitude = 21.21;

            try
            {
                // Fetch the weather data
                var weatherData = await _weatherService.GetWeatherAsync(latitude, longitude);

                // Display current weather 
                var current = weatherData.Current;
                if (current != null)
                {
                    minTempLabel.Text = $"Current Temp: {current.Temperature2m}°C"; // Adjusted to match the property name
                }

                // Display hourly weather 
                var hourly = weatherData.Hourly;
                if (hourly != null && hourly.Temperature2m != null && hourly.Temperature2m.Count > 0)
                {
                    rainLabel.Text = $"First Hour Temp: {hourly.Temperature2m[0]}°C, Wind Speed: {hourly.WindSpeed1000hPa[0]} m/s";
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