namespace BackgroundTaskHandlerAPI.Models.Tasks.ManualTasks;

public class Task2(ILogger<Task2> logger) : TaskBase
{
    public override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"{this.GetType().Name} - {nameof(Task2)}");
        return Task.CompletedTask;
    }
}