using Microsoft.Maui.Controls;

namespace WeatherApp
{
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadSummary();
        }

        private async void LoadSummary()
        {
            // Define the coordinates for the weather data (e.g., Vaasa, Finland)
            double latitude = 63.096;   // Replace with your desired latitude
            double longitude = 21.6158; // Replace with your desired longitude

            try
            {
                // Fetch detailed weather data
                var weatherData = await WeatherService.GetWeatherData(latitude, longitude);

                // Display current weather
                var current = weatherData.Current;
                if (current != null)
                {
                    weatherSummaryLabel.Text = $"Current Temp: {current.Temperature2m}°C, Wind Speed: {current.WindSpeed10m} m/s";
                }

                // Display hourly summary (e.g., first entry)
                var hourly = weatherData.Hourly;
                if (hourly != null && hourly.Temperature2m != null && hourly.Temperature2m.Count > 0)
                {
                    weatherSummaryLabel.Text += $"\nFirst Hour Temp: {hourly.Temperature2m[0]}°C, Humidity: {hourly.RelativeHumidity2m[0]}%";
                }
            }
            catch (Exception ex)
            {
                weatherSummaryLabel.Text = $"Unable to load weather data: {ex.Message}";
            }

            // Load todo summary
            todoSummaryLabel.Text = $"Tasks Today: {TodoService.GetTasksCount()}";
        }

        private async void OnWeatherPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void OnTodoPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TodoPage());
        }
    }
}
