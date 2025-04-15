namespace BackgroundTaskHandlerAPI.Errors.Types;

public enum ErrorType
{
    Unknown,
    DatabaseError,
    ValidationError,
    NetworkError,
    TaskError,
    InvalidKeyError,
    InvalidHostedServiceError,
    AlreadyRunningError,
    AlreadyStoppedError,
    InvalidTaskBaseError,
    NoTaskRegisteredError,
    TaskDuplicatedError,
    InvalidTaskNameError,
    RegisterTaskError
}