
    public interface ICalendarService
    {
        List<TaskModel> GetAllTasks();
        TaskModel AddTask(TaskModel task);
        bool DeleteTask(int id);
        TaskModel UpdateTask(int id, TaskModel task);
    }

    public class CalendarService : ICalendarService
    {
        private readonly List<TaskModel> _tasks = new List<TaskModel>(); // Replace with database logic

        public List<TaskModel> GetAllTasks() => _tasks;

        public TaskModel AddTask(TaskModel task)
        {
            task.Id = _tasks.Count + 1;
            _tasks.Add(task);
            return task;
        }

        public bool DeleteTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
                return true;
            }
            return false;
        }

        public TaskModel UpdateTask(int id, TaskModel task)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask != null)
            {
                existingTask.Description = task.Description;
                existingTask.Date = task.Date;
                return existingTask;
            }
            return null;
        }
    }

public class TaskModel
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}


