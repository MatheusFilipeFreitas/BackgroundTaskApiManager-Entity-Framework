using BackgroundTaskHandlerAPI.Errors.Types;

namespace BackgroundTaskHandlerAPI.Errors;

public static class ErrorMessages
{
    private static readonly Dictionary<ErrorType, string> _errorMessages = new()
    {
        { ErrorType.Unknown, "An unknown error has occurred." },
        { ErrorType.DatabaseError, "A database error has occurred." },
        { ErrorType.ValidationError, "Validation error has occurred." },
        { ErrorType.NetworkError, "A network error has occurred." },
        { ErrorType.TaskError, "An error occured while processing the task." },
        { ErrorType.InvalidKeyError, "Invalid key." },
        { ErrorType.InvalidHostedServiceError, "Invalid hosted service." },
        { ErrorType.AlreadyRunningError, "Task is already running." },
        { ErrorType.AlreadyStoppedError, "Task is already stopped." },
        { ErrorType.InvalidTaskBaseError, "Could not get/use this task base." },
        { ErrorType.NoTaskRegisteredError, "No registered task found." },
        { ErrorType.TaskDuplicatedError, "Found duplicated task." },
        { ErrorType.RegisterTaskError, "Could not register task." },
        { ErrorType.InvalidTaskNameError, "Task name is invalid. Cannot be null or empty." },
    };

    public static string GetErrorMessage(ErrorType errorType)
    {
        var outputMessage = _errorMessages.TryGetValue(errorType, out var message) ? message : _errorMessages[ErrorType.Unknown];
        return "[Api base Error]: " + outputMessage;
    }

    public static string GetErrorMessage(ErrorType errorType, string taskName)
    {
        var outputMessage = _errorMessages.TryGetValue(errorType, out var message) ? message : _errorMessages[errorType];
        return "[Task Error - " + taskName + "]: " + outputMessage;
    }
}