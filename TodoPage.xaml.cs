/*using Microsoft.Maui.Controls;
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
}*/


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
            LoadTasks();
        }

        private void LoadTasks()
        {
            todoItems.Clear();
            foreach (var task in TodoService.GetPendingTasks())
            {
                todoItems.Add(task);
            }
            todoListView.ItemsSource = todoItems;
        }

        private async void OnAddTaskClicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("New Task", "Enter task description:");
            if (!string.IsNullOrWhiteSpace(result))
            {
                TodoService.AddTask(result);
                LoadTasks();
            }
        }

        private void OnCompleteTaskClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is TodoItem task)
            {
                TodoService.MarkTaskComplete(task.Task);
                LoadTasks();
            }
        }

        private async void OnTaskTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is TodoItem task)
            {
                await DisplayAlert("Task Details", $"Task: {task.Task}\nDue: {task.DueDate}", "OK");
            }
        }
    }
}

