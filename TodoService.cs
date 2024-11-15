using System.Collections.Generic;

namespace WeatherApp
{
    public static class TodoService
    {
        private static readonly List<string> Todos = new List<string>();

        public static int GetTasksCount() => Todos.Count;

        public static void AddTask(string task) => Todos.Add(task);
    }
}
