using System.Reflection;
using BackgroundTaskHandlerAPI.Models;
using BackgroundTaskHandlerAPI.Models.Types;
using BackgroundTaskHandlerAPI.Services;
using BackgroundTaskHandlerAPI.Utils;

namespace BackgroundTaskHandlerAPI.Configuration.Initializer;

public class ManualTaskAutomaticRegister(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var tasksService = scope.ServiceProvider.GetRequiredService<ITasksService>();

        var manualTaskTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => 
                t.Namespace == "BackgroundTaskHandlerAPI.Models.Tasks.ManualTasks" &&
                typeof(IBackgroundTask).IsAssignableFrom(t) &&
                !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var type in manualTaskTypes)
        {
            if (scope.ServiceProvider.GetService(type) is not IBackgroundTask taskInstance) continue;
            var taskInfo = new TaskInformation(taskInstance, TaskType.MANUAL, string.Empty);
            await tasksService.AddTaskAsync(taskInfo);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}