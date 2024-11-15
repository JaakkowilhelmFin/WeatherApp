using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace WeatherApp
{
    public partial class TodoPage : ContentPage
    {
        private ObservableCollection<TodoItem> todoItems = new ObservableCollection<TodoItem>();

        public TodoPage()
        {
            InitializeComponent();
            todoListView.ItemsSource = todoItems;
        }

        private async void OnAddTaskClicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("New Task", "Enter task description:");
            if (!string.IsNullOrWhiteSpace(result))
            {
                todoItems.Add(new TodoItem { Task = result, DueDate = DateTime.Now.ToString("MMM dd") });
            }
        }
    }

    public class TodoItem
    {
        public string Task { get; set; }
        public string DueDate { get; set; }
    }
}
