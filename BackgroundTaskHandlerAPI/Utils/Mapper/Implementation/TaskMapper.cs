using BackgroundTaskHandlerAPI.Models;
using BackgroundTaskHandlerAPI.Models.Types;

namespace BackgroundTaskHandlerAPI.Utils.Mapper.Implementation;

public class TaskMapper : ITaskMapper
{
    public TaskResume ToTaskResume<T>(T task)
    {
        var taskNameProp = task?.GetType().GetProperty("TaskName");
        var taskStatusProp = task?.GetType().GetProperty("Status");

        if (taskNameProp == null || taskStatusProp == null)
        {
            throw new InvalidOperationException("Cannot map class. Missing properties: TaskName or Status.");
        }

        var taskName = taskNameProp.GetValue(task)?.ToString();
        var taskStatus = taskStatusProp.GetValue(task) is StatusType ? (StatusType)taskStatusProp.GetValue(task)! : StatusType.WAITING;

        return new TaskResume(taskName!, taskStatus);
    }
}