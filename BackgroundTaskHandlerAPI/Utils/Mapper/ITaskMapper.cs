using BackgroundTaskHandlerAPI.Models;

namespace BackgroundTaskHandlerAPI.Utils.Mapper;

public interface ITaskMapper
{
    TaskResume ToTaskResume<T>(T task);
}