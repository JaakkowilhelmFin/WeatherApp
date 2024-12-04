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
            LoadTaskSummary();
        }

        private void LoadTaskSummary()
        {
            // Assuming TodoService is defined and GetTasksCount() returns the number of tasks
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