namespace BackgroundTaskHandlerAPI.Models.Tasks.StartupTasks;

public class Task1(ILogger<Task1> logger) : TaskBase
{
    public override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation($"{this.GetType().Name} - {nameof(Task1)} está em execução.");
            try
            {
                await Task.Delay(3000, stoppingToken);
            }
            catch (TaskCanceledException)
            {
                logger.LogInformation($"{this.GetType().Name} foi cancelada.");
                break;
            }
        }
    }
}