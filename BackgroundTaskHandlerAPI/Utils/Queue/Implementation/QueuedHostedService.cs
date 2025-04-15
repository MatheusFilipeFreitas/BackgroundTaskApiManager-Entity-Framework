namespace BackgroundTaskHandlerAPI.Utils.Queue.Implementation;

public class QueuedHostedService(IBackgroundTaskQueue taskQueue) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var workItem = await taskQueue.DequeueAsync(stoppingToken);
            await workItem(stoppingToken);
        }
    }
}