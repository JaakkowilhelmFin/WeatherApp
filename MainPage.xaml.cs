using System;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public MainPage()
        {
            InitializeComponent();
            GetWeatherData();
        }

        public async void GetWeatherData()
        {
            double latitude = 35.6895;   
            double longitude = 139.6917;  

            try
            {
                

                string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=temperature_2m_max,temperature_2m_min,precipitation_sum,windspeed_10m_max&timezone=auto";
                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<WeatherResponse>(json);

                DisplayWeatherData(weatherData);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Unable to retrieve weather data: {ex.Message}", "OK");
            }
        }

        public void DisplayWeatherData(WeatherResponse weatherData)
        {
            if (weatherData?.Daily != null)
            {
                minTempLabel.Text = $"Min Temp: {weatherData.Daily.Temperature2mMin[0]}°C";
                maxTempLabel.Text = $"Max Temp: {weatherData.Daily.Temperature2mMax[0]}°C";
                rainLabel.Text = $"Rain: {weatherData.Daily.PrecipitationSum[0]} mm";

                // Check for wind alert
                if (weatherData.Daily.Windspeed10mMax[0] > 4)
                {
                    windAlertLabel.Text = "Warning: Wind speed exceeds 4 m/s!";
                    windAlertLabel.TextColor = Colors.Red;
                }
                else
                {
                    windAlertLabel.Text = "Wind speed is safe today.";
                }
            }
        }
    }

    // Model for deserializing JSON response
    public class WeatherResponse
    {
        public Daily? Daily { get; set; }
    }

    public class Daily
    {
        public List<double>? Temperature2mMin { get; set; }
        public List<double>? Temperature2mMax { get; set; }
        public List<double>? PrecipitationSum { get; set; }
        public List<double>? Windspeed10mMax { get; set; }
    }
}
