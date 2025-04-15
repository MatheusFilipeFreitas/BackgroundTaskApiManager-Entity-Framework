using BackgroundTaskHandlerAPI.Errors;
using BackgroundTaskHandlerAPI.Errors.Types;
using BackgroundTaskHandlerAPI.Models;
using BackgroundTaskHandlerAPI.Models.Types;
using BackgroundTaskHandlerAPI.Utils.Mapper;
using BackgroundTaskHandlerAPI.Utils.Queue;

namespace BackgroundTaskHandlerAPI.Services.Implementation;

public class TasksService(IBackgroundTaskQueue tasksQueue, ITaskMapper mapper) : ITasksService
{
    private readonly HashSet<TaskInformation> _tasks = new HashSet<TaskInformation>();
    
    public Task<IEnumerable<TaskResume>> GetTasksAsync()
    {
        try
        {
            return Task.FromResult(this.GetTaskListHandler().Select(mapper.ToTaskResume));
        }
        catch
        {
            throw;
        }
    }
    
    public Task<bool> FindTaskByNameAsync(string taskName)
    {
        try
        {
            this.GetTaskFromListHandler(taskName);
            return Task.FromResult(true);
        }
        catch (KeyNotFoundException keyNotFoundException)
        {
            return Task.FromResult(false);
        }
        catch (Exception exception)
        {
            throw;
        }
    }
    
    public Task AddTaskAsync(TaskInformation task)
    {
        try
        {
            this.AddTaskIntoListHandler(task);
            return Task.CompletedTask;
        }
        catch
        {
            throw;
        }
    }

    public async Task<TaskResume> StartTaskAsync(string taskName)
    {
        try
        {
            var task = this.GetTaskFromListHandler(taskName);
            await this.EnqueueTaskHandler(task);
            return mapper.ToTaskResume(task);
        }
        catch
        {
            throw;
        }
    }

    public async Task<TaskResume> StopTaskAsync(string taskName)
    {
        try
        {
            var task = this.GetTaskFromListHandler(taskName);
            await this.CancelTaskHandler(task);
            return mapper.ToTaskResume(task);
        }
        catch
        {
            throw;
        }
    }
    
    // Handlers
    
    private void AddTaskIntoListHandler(TaskInformation task)
    {
        _tasks.Add(task);
    }

    private TaskInformation GetTaskFromListHandler(string taskName)
    {
        return _tasks.FirstOrDefault(t => t.TaskName == taskName) 
               ?? throw new KeyNotFoundException(ErrorMessages.GetErrorMessage(ErrorType.NoTaskRegisteredError));
    }

    private IEnumerable<TaskInformation> GetTaskListHandler()
    {
        return _tasks.ToList();
    }

    private async Task EnqueueTaskHandler(TaskInformation task)
    {
        if (task.Status == StatusType.RUNNING)
        {
            throw new InvalidOperationException(ErrorMessages.GetErrorMessage(ErrorType.AlreadyRunningError));
        }
        task.Status = StatusType.RUNNING;
        
        var workItem = new Func<CancellationToken, Task>(async _ =>
        {
            try
            {
                await task.TaskAction.ExecuteAsync(task.CancellationTokenSource.Token);
                task.Status = StatusType.SUCCESS;
            }
            catch (OperationCanceledException)
            {
                task.Status = StatusType.CANCELLED;
            }
            catch (Exception ex)
            {
                task.Status = StatusType.FAILED;
                throw new ApplicationException(ErrorMessages.GetErrorMessage(ErrorType.TaskError));
            }
        });
        
        await tasksQueue.QueueBackgroundWorkItemAsync(workItem);
    }

    private async Task CancelTaskHandler(TaskInformation task)
    {
        if (task.Status != StatusType.RUNNING)
        {
            throw new InvalidOperationException(ErrorMessages.GetErrorMessage(ErrorType.AlreadyStoppedError));
        }

        await task.CancellationTokenSource.CancelAsync();
            task.Status = StatusType.CANCELLED;
            task.CancellationTokenSource = new CancellationTokenSource();
    }
}