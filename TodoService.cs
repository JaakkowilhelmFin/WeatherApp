/*using System.Collections.Generic;

namespace WeatherApp
{
    public static class TodoService
    {
        private static readonly List<string> Todos = new List<string>();

        public static int GetTasksCount() => Todos.Count;

        public static void AddTask(string task) => Todos.Add(task);
    }
}*/


using System.Collections.Generic;
using System.Linq;

namespace WeatherApp
{
    public static class TodoService
    {
        private static readonly List<TodoItem> Todos = new List<TodoItem>();

        public static int GetTasksCount() => Todos.Count(t => !t.IsCompleted);

        public static void AddTask(string task) =>
            Todos.Add(new TodoItem { Task = task, DueDate = DateTime.Now.ToString("MMM dd"), IsCompleted = false });

        public static List<TodoItem> GetPendingTasks() =>
            Todos.Where(t => !t.IsCompleted).ToList();

        public static void MarkTaskComplete(string task)
        {
            var todo = Todos.FirstOrDefault(t => t.Task == task);
            if (todo != null)
            {
                todo.IsCompleted = true;
            }
        }
    }

    public class TodoItem
    {
        public string Task { get; set; }
        public string DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}

