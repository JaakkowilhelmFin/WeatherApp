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
                var weatherSummary = await WeatherService.GetWeatherSummary(latitude, longitude);

                // Display data in UI labels
                minTempLabel.Text = $"Min Temp: {weatherSummary.MinTemp}°C";
                maxTempLabel.Text = $"Max Temp: {weatherSummary.MaxTemp}°C";
                rainLabel.Text = $"Rain: {weatherSummary.Rain} mm";
            }
            catch (Exception ex)
            {
                // Display error message
                await DisplayAlert("Error", $"Unable to retrieve weather data: {ex.Message}", "OK");
            }
        }
    }
}
