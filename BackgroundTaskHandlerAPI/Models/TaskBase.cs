using BackgroundTaskHandlerAPI.Utils;

namespace BackgroundTaskHandlerAPI.Models;

public class TaskBase : IBackgroundTask
{
    public virtual async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}