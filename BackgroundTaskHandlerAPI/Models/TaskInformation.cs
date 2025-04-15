using BackgroundTaskHandlerAPI.Models.Types;
using BackgroundTaskHandlerAPI.Utils;

namespace BackgroundTaskHandlerAPI.Models;

public class TaskInformation(IBackgroundTask taskAction, TaskType? type = null, string? description = null)
{
    public string TaskName { get; set; } = taskAction.GetType().Name;
    public string TaskDescription { get; set; } = description ?? string.Empty;
    public IBackgroundTask TaskAction { get; set; } = taskAction;
    public TaskType Type { get; set; } = type ?? TaskType.MANUAL;
    public StatusType Status { get; set; } = StatusType.WAITING;
    public CancellationTokenSource CancellationTokenSource { get; set; } = new CancellationTokenSource();
}