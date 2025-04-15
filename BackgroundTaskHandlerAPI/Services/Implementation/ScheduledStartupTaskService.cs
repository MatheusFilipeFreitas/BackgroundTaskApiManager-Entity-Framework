using BackgroundTaskHandlerAPI.Errors;
using BackgroundTaskHandlerAPI.Errors.Types;
using BackgroundTaskHandlerAPI.Models.Configs;
using BackgroundTaskHandlerAPI.Utils;

namespace BackgroundTaskHandlerAPI.Services.Implementation;

public class ScheduledStartupTaskService(
    IServiceProvider serviceProvider,
    ILogger<ScheduledStartupTaskService> logger,
    IEnumerable<StartupTaskConfig> startupTasks,
    Dictionary<string, CancellationTokenSource> runningTasks
) : BackgroundService
{
    private const int DelayInSeconds = 60;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this.CheckInitialTasksConfiguration();
        while (!stoppingToken.IsCancellationRequested)
        {
            await this.CheckAndManageTasksAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(DelayInSeconds), stoppingToken);
        }
    }

    private void CheckInitialTasksConfiguration()
    {
        foreach (var config in startupTasks)
        {
            this.FindTaskInProject(config.Name);
        }
    }

    private async Task CheckAndManageTasksAsync(CancellationToken stoppingToken)
    {
        foreach (var config in startupTasks)
        {
            var now = DateTime.Now.TimeOfDay;
            if (this.IsWithinExecutionWindow(now, config.StartTime, config.StopTime))
            {
                await this.StartTaskIfNotRunningAsync(config);
            }
            else
            {
                this.StopTaskIfRunning(config.Name);
            }
        }
    }

    private bool IsWithinExecutionWindow(TimeSpan now, TimeSpan start, TimeSpan stop)
    {
        if (start < stop)
        {
            return now >= start && now < stop;
        }
        else
        {
            return now >= start || now < stop;
        }
    }

    private async Task StartTaskIfNotRunningAsync(StartupTaskConfig config)
    {
        if (runningTasks.ContainsKey(config.Name)) return;

        var cts = new CancellationTokenSource();
        runningTasks[config.Name] = cts;

        _ = Task.Run(async () => await this.ExecuteTaskAsync(config.Name, cts.Token), cts.Token);
    }

    private void StopTaskIfRunning(string taskName)
    {
        if (!runningTasks.ContainsKey(taskName)) return;

        logger.LogInformation("Stopping task '{TaskName}'...", taskName);
        runningTasks[taskName].Cancel();
        runningTasks[taskName].Dispose();
        runningTasks.Remove(taskName);
    }

    private async Task ExecuteTaskAsync(string taskName, CancellationToken token)
    {
        try
        {
            var taskType = this.FindTaskInProject(taskName);

            if (taskType == null || !typeof(IBackgroundTask).IsAssignableFrom(taskType))
            {
                logger.LogWarning("['{TaskName}'] -" + ErrorMessages.GetErrorMessage(ErrorType.InvalidHostedServiceError), taskName);
                return;
            }

            using var scope = serviceProvider.CreateScope();

            var taskInstance = ActivatorUtilities.CreateInstance(scope.ServiceProvider, taskType!) as IBackgroundTask;

            if (taskInstance is null)
            {
                logger.LogWarning("['{TaskName}'] - " + ErrorMessages.GetErrorMessage(ErrorType.RegisterTaskError), taskName);
                return;
            }

            await taskInstance.ExecuteAsync(token);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ErrorMessages.GetErrorMessage(ErrorType.TaskError));
        }
    }

    private Type? FindTaskInProject(string taskName)
    {
        var taskType = ScheduledStartupTaskService.GetTaskType(taskName);
        if (taskType == null)
        {
            throw new Exception(ErrorMessages.GetErrorMessage(ErrorType.NoTaskRegisteredError));
        }
        return taskType;
    }

    private static Type? GetTaskType(string taskName)
    {
        return Type.GetType($"BackgroundTaskHandlerAPI.Models.Tasks.StartupTasks.{taskName}");
    }
}