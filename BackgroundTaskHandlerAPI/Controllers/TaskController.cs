using BackgroundTaskHandlerAPI.Models;
using BackgroundTaskHandlerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundTaskHandlerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController(ITasksService service) : ApiBaseController
{
    [HttpGet]
    public Task<IActionResult> GetByRegisteredName()
    {
        return ApiResponse<IEnumerable<TaskResume>>(service.GetTasksAsync());
    }

    [HttpPut("start/{taskName}")]
    public Task<IActionResult> StartTaskByRegisteredName([FromRoute] string taskName)
    {
        return ApiResponse<TaskResume>
        (
            service.StartTaskAsync(taskName), 
            service.FindTaskByNameAsync(taskName), 
            taskName
        );
    }

    [HttpPut("stop/{taskName}")]
    public Task<IActionResult> StopTaskByRegisteredName([FromRoute] string taskName)
    {
        return ApiResponse<TaskResume>
        (
            service.StopTaskAsync(taskName), 
            service.FindTaskByNameAsync(taskName), 
            taskName
        );
    }
    
}