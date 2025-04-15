namespace BackgroundTaskHandlerAPI.Models.Configs;

public class StartupTaskConfig
{
    public string Name { get; set; } = string.Empty;
    public TimeSpan StartTime { get; set; } = TimeSpan.Zero;
    public TimeSpan StopTime { get; set; } = TimeSpan.Zero;
}