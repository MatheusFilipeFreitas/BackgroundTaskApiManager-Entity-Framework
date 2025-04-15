using BackgroundTaskHandlerAPI.Models;

namespace BackgroundTaskHandlerAPI.Services;

public interface ITasksService
{
    Task<IEnumerable<TaskResume>> GetTasksAsync();
    Task<bool> FindTaskByNameAsync(string taskName);
    Task AddTaskAsync(TaskInformation task);
    Task<TaskResume> StartTaskAsync(string taskName);
    Task<TaskResume> StopTaskAsync(string taskName);
}