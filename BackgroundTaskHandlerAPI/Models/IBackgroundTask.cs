namespace BackgroundTaskHandlerAPI.Utils;

public interface IBackgroundTask
{
    Task ExecuteAsync(CancellationToken stoppingToken);
}