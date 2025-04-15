using BackgroundTaskHandlerAPI.Models.Types;

namespace BackgroundTaskHandlerAPI.Models;

public class TaskResume (string taskName, StatusType status)
{
    public string TaskName { get; set; } = taskName;
    public StatusType Status { get; set; } = status;
}