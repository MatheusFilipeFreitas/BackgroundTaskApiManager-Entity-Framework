using BackgroundTaskHandlerAPI.Errors;
using BackgroundTaskHandlerAPI.Errors.Types;
using BackgroundTaskHandlerAPI.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundTaskHandlerAPI.Controllers;

public abstract class ApiBaseController : Controller
{
    protected async Task<IActionResult> ApiResponse<T>(Task<T> result, Task<bool>? existsFunction = null, string? taskName = null)
    {
        if (!string.IsNullOrWhiteSpace(taskName) && existsFunction != null && !await existsFunction)
        {
            return NotFound(CreateErrorResponse<T>(ErrorType.NoTaskRegisteredError));
        }

        if (string.IsNullOrWhiteSpace(taskName))
        {
            return BadRequest(CreateErrorResponse<T>(ErrorType.InvalidTaskNameError));
        }

        try
        {
            var data = await result;

            if (data != null)
            {
                return Ok(new ResponseModel<T>(true, data));
            }

            return BadRequest(CreateErrorResponse<T>(ErrorType.TaskError));
        }
        catch (Exception ex)
        {
            var message = ex.Message ?? ErrorMessages.GetErrorMessage(ErrorType.Unknown);
            return BadRequest(new ResponseModel<T?>(false, default, message));
        }
    }
    
    protected Task<IActionResult> ApiResponse<T>(Task<T> result, string taskName) =>
        ApiResponse(result, null, taskName);

    protected Task<IActionResult> ApiResponse<T>(Task<T> result) =>
        ApiResponse(result, null, "Default");
    
    private ResponseModel<T?> CreateErrorResponse<T>(ErrorType errorType) =>
        new(false, default, ErrorMessages.GetErrorMessage(errorType));
}