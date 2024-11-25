/*using Microsoft.Maui.Controls;

namespace WeatherApp
{
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
            LoadSummary();
        }

        private async void LoadSummary()
        {
            // Load basic weather data
            try
            {
                var weatherData = await WeatherService.GetWeatherSummary();
                weatherSummaryLabel.Text = $"Min Temp: {weatherData.MinTemp}°C, Max Temp: {weatherData.MaxTemp}°C, Rain: {weatherData.Rain} mm";
            }
            catch
            {
                weatherSummaryLabel.Text = "Unable to load weather data.";
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
}*/

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
            // Load basic weather data
            try
            {
                var weatherData = await WeatherService.GetWeatherSummary();
                weatherSummaryLabel.Text = $"Min Temp: {weatherData.MinTemp}°C, Max Temp: {weatherData.MaxTemp}°C, Rain: {weatherData.Rain} mm";
            }
            catch
            {
                weatherSummaryLabel.Text = "Unable to load weather data.";
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

